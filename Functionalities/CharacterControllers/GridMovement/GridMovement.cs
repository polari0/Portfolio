using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset actionsAsset;

    [SerializeField]
    private InputAction moveAction;


    //make sure we are the player 
    [SerializeField]
    private bool isPlayerControlled;


    //variables needed for the controlling of the character
    [SerializeField]
    private Transform movePoint;


    /// <summary>
    /// In awake we gotta subrcipe all the events so we can move the character with new input system.
    /// </summary>
    private void Awake()
    {
        movePoint.parent = null;
        //Here we find all the actions needed for the inputs so we are able to use them. 
        actionsAsset.FindActionMap("MovementInputs").FindAction("Use").performed += OnUse;
        actionsAsset.FindActionMap("MovementInputs").FindAction("WayPointUp").performed += OnWayPointUp;
        actionsAsset.FindActionMap("MovementInputs").FindAction("WayPointDown").performed += OnWayPointDown;
        actionsAsset.FindActionMap("MovementInputs").FindAction("WayPointLeft").performed += OnWayPointLeft;
        actionsAsset.FindActionMap("MovementInputs").FindAction("WayPointRight").performed += OnWayPointRight;
    }

    #region onDisable/Eneable
    private void OnEnable()
    {
        actionsAsset.FindActionMap("MovementInputs").Enable();
    }
    private void OnDisable()
    {
        actionsAsset.FindActionMap("MovementInputs").Disable();
    }
    #endregion


    #region All the different movement actions and their effects. 
    private void OnUse(InputAction.CallbackContext context)
    {
        //what happens when we use something
         Debug.Log("We used"); 
    }

    #region Up/Down/Left/Right buttons
    private void OnWayPointUp(InputAction.CallbackContext context)
    {
        movePoint.position += new Vector3(0f, 0f, 1f);
    }
    private void OnWayPointDown(InputAction.CallbackContext context)
    {
         movePoint.position += new Vector3(0f, 0f, -1f);
    }
    private void OnWayPointLeft(InputAction.CallbackContext context)
    {
        movePoint.position += new Vector3(-1f, 0f, 0f);
    }
    private void OnWayPointRight(InputAction.CallbackContext context)
    {
        movePoint.position += new Vector3(1f, 0f, 0f);
    }
    #endregion
    #endregion
}
