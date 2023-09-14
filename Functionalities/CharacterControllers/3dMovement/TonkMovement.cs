using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// This is simple character controller for physics based 3d movement 
/// </summary>
public class TonkMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset actionsAsset;

    [SerializeField]
    private InputAction moveAction;

    //variables needed for the controlling of the character
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private int speed;


    private void Awake()
    {
        //Here we find all the actions needed for the inputs so we are able to use them. 
        moveAction = actionsAsset.FindActionMap("MovementInputs").FindAction("Move") ;
        actionsAsset.FindActionMap("MovementInputs").FindAction("Jump").performed += OnJump;
        actionsAsset.FindActionMap("MovementInputs").FindAction("Crouch").performed += OnCrouch;
        actionsAsset.FindActionMap("MovementInputs").FindAction("Use").performed += OnUse;
        actionsAsset.FindActionMap("MovementInputs").FindAction("Roll").performed += OnRoll;
    }

    private void OnEnable()
    {
        actionsAsset.FindActionMap("MovementInputs").Enable();
    }

    private void OnDisable()
    {
        actionsAsset.FindActionMap("MovementInputs").Disable();
    }
    #region All the different movement actions and their effects. 

    private void OnJump(InputAction.CallbackContext context)
    {
        //What happens when jump is pressed. 
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("We should never need to use this");
    }

    private void OnUse(InputAction.CallbackContext context)
    {
       //what happens when we use the object we are pointing at. 
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        Debug.Log("We should never need to use this");
    }


    #endregion
    private void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        rb.transform.position = transform.position + new Vector3(moveVector.x, 0, moveVector.y)* _sparklerStats.speed * Time.deltaTime;
    }
}
