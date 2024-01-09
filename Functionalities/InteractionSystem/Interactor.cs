using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
 * Know issues
 * This code allows player to trow items out of the map if they move it too close to the wall 
 * This how ever is players skill issue if you ask from me. 
 * How I handle ignored collisions might cause issues later on but then we can look into ignoring collision on specific layers 
*/


namespace HexKeyGames.InteractionSystem
{
    /// <summary>
    /// This script handles the Interaction Mechanics of the game as well as pickup mechanics of the game. We can pick stuff up as well as drop them. 
    /// TO DO If picked up item has special property do something with it. 
    /// </summary>
    public class Interactor : MonoBehaviour
    {
        #region Inspector parameters
        [SerializeField, Tooltip("Camera attached to player transform used for raycast")]
        private Camera playerCamera;

        [SerializeField, Tooltip("Input actions asset")]
        private InputActionAsset characterControlsActionAsset;

        [SerializeField, Tooltip("FirstPersonCharacterController component of the player character")]
        private CharacterMovement.FirstPersonController FPSController;

        [SerializeField, Tooltip("Max distance you can interact with objects")]
        private float interactRange = 2f;

        [SerializeField, Tooltip("GameObject (in UI) that will be enabled/disabled when player can interact with an interactable")]
        private GameObject interactableIndicator;

        [SerializeField, Tooltip("Where the item is positioned at when being carried")]
        public Transform carryPosition;

        [SerializeField, Tooltip("How often position is updated")]
        private float smooth = 1;

        [SerializeField, Range(0, 1000), Tooltip("FORWARD impulse applied to an object when thrown with full power (newton seconds)")]
        private float maxThrowPowerForward;

        [SerializeField, Range(0, 1000), Tooltip("UPWARD impulse applied to an object when thrown with full power (newton seconds)")]
        private float maxThrowPowerUp;

        [SerializeField, Range(0.01f, 2.0f), Tooltip("How sensitive player inputs are when rotating objects")]
        private float itemRotationSensitivity = 1.0f;

        [SerializeField, Range(90f, 1800f), Tooltip("Maximum rotation rate of rotated objects")]
        private float itemRotationMaxRate = 900f;
        #endregion

        private bool Carrying => carriedObject != null;
        private Rigidbody carriedObjRb;
        public GameObject carriedObject;
        private IInteractable interactable;
        private bool rotate = false;
        private Vector3 lastCarriedObjDirection; // Direction of the carried object in the last frame, used for rotation.



        private void Awake()
        {
            if (FPSController == null) FPSController = GetComponent<CharacterMovement.FirstPersonController>();

            characterControlsActionAsset.FindActionMap("Character").FindAction("PrimaryInteract").performed += OnPrimaryInteract;
            characterControlsActionAsset.FindActionMap("Character").FindAction("SecondaryInteract").performed += OnSecondaryInteract;
        }

        /// <summary>
        /// OnInteract is what happends when we press primary interaction button. Simply but we check if the object is interactable and or can be picked up. 
        /// Based on these we change player movement behaviour. And call the interaction on the object. This most likely still needs to have few iterations 
        /// since right now we can't interact with other objects while carrying something. For now this is fine
        /// </summary>
        /// <param name="context">Makes sure we can call the function when button is pressed</param>
        private void OnPrimaryInteract(InputAction.CallbackContext context)
        {
            if (!context.ReadValueAsButton()) // if released
            {
                if (Carrying)
                {
                    if(carriedObject.GetComponent<Pickupable>())
                    {
                        float heldDuration = (float)context.duration;
                        float power = heldDuration / 5f;
                        ThrowObject(power);
                    }

                    if(carriedObject.GetComponent<HandHeldScanner>())
                    {
                        (interactable as IPrimaryInteractable).PrimaryInteract();
                    }
                }
                else
                {
                    if (interactable is HandHeldScanner)
                    {
                        carriedObject = (interactable as HandHeldScanner).gameObject;
                        (interactable as IPrimaryInteractable).PrimaryInteract();
                    }

                    else if (interactable is Pickupable)
                    {
                        // TODO: create a method from this code block
                        carriedObject = (interactable as Pickupable).gameObject;
                        carriedObjRb = carriedObject.GetComponent<Rigidbody>();
                        carriedObjRb.isKinematic = true;
                        Physics.IgnoreCollision(carriedObjRb.GetComponent<Collider>(), GetComponent<Collider>(), true); // TODO: do this for all carried obj's colliders, use layers
                        lastCarriedObjDirection = playerCamera.transform.forward;
                    }
                    else if (interactable is IPrimaryInteractable)
                        (interactable as IPrimaryInteractable).PrimaryInteract();
                }
            }
        }

        /// <summary>
        /// secondary interaction is bit different. Here we first check if we already are holding something and wether or not it can be interacted with interaction2. 
        /// If we are we check if it is tool or not if it is tool we use it's secondary interaction otherwise we drop it.
        /// If we are holding nothing we check if the object we are pointing at can be interacted with. 
        /// </summary>
        /// <param name="context"></param>
        private void OnSecondaryInteract(InputAction.CallbackContext context)
        {
            if (Carrying)
            {
                rotate = context.ReadValueAsButton();
                FPSController.cameraCanMove = !rotate;
            }

            else if (interactable is ISecondaryInteractable)
                (interactable as ISecondaryInteractable).SecondaryInteract();
        }

        private void Update()
        {
            if (Carrying)
            {
                if (carriedObject.GetComponent<Pickupable>() != null)
                {
                    Carry();
                    if (rotate)
                        RotateCarriedObj();
                }

                else if (carriedObject.GetComponent<HandHeldScanner>() != null)
                {
                    // Do nothing
                    return;
                }
            }

            else
            {
                Ray ray = new(
                    playerCamera.transform.position,
                    playerCamera.transform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
                    hit.collider.TryGetComponent(out interactable);
                else
                    interactable = null;

                if (interactableIndicator != null)
                    interactableIndicator.SetActive(interactable != null);
            }


        }

        /// <summary>
        /// This function handles the carrying of the object. 
        /// </summary>
        /// <param name="o"></param>
        private void Carry()
        {
            // Move
            Vector3 newPosition = Vector3.MoveTowards(carriedObject.transform.position, carryPosition.position, Time.deltaTime * smooth);
            carriedObject.transform.position = newPosition;

            // Rotate
            Vector3 newForward = newPosition - playerCamera.transform.position;
            float yaw = Vector3.SignedAngle(
                Vector3.ProjectOnPlane(lastCarriedObjDirection, Vector3.up), 
                Vector3.ProjectOnPlane(newForward, Vector3.up), 
                Vector3.up);
            carriedObject.transform.Rotate(Vector3.up, yaw, Space.World);
            lastCarriedObjDirection = newForward;
        }

        /// <summary>
        /// Rotate object with look input (mouse/right stick).
        /// </summary>
        private void RotateCarriedObj()
        {
            float maxRate = itemRotationMaxRate * Time.deltaTime;
            float yaw =
                Mathf.Clamp(
                    -FPSController.mouseDeltaInput.x * itemRotationSensitivity,
                    -maxRate,
                    maxRate);

            float pitch =
                Mathf.Clamp(
                    FPSController.mouseDeltaInput.y * itemRotationSensitivity,
                    -maxRate,
                    maxRate);

            carriedObject.transform.Rotate(Vector3.up, yaw, Space.World);
            carriedObject.transform.Rotate(playerCamera.transform.right, pitch, Space.World);
        }

        /// <summary>
        /// Throws the carried object, or drops it if power is zero.
        /// </summary>
        /// <param name="power">How much power the object is thrown with. Internally clamped between 0 and 1. 0 = drop, 1.0 = full power.</param>
        private void ThrowObject(float power)
        {
            power = Mathf.Clamp01(power);
            carriedObjRb.isKinematic = false;
            Physics.IgnoreCollision(carriedObjRb.GetComponent<Collider>(), GetComponent<Collider>(), false); // TODO: do this for all carried obj's colliders, use layers
            carriedObjRb.AddForce(GetThrowForce(power), ForceMode.Impulse);
            carriedObject = null;
            carriedObjRb = null;
            rotate = false;
            FPSController.cameraCanMove = true;
        }

        private Vector3 GetThrowForce(float power)
        {
            Vector3 fwd = playerCamera.transform.forward * maxThrowPowerForward;
            Vector3 up = Vector3.up * maxThrowPowerUp;
            Vector3 force = (fwd + up) * power;
            return force;
        }
    }
}