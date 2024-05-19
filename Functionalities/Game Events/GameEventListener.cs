using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Don't worry about this for now we are exteding the unity event class a little bit. 
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}

/// <summary>
/// think of it as radio reciever tuned to correct channel. Can be used multiple times in one script if needed bit messy but works really well.
/// </summary>
public class GameEventListener : MonoBehaviour
{

    [Tooltip("Event to register with.")]
    public GameEvent gameEvent;

    [Tooltip("Response to invoke when Event with GameData is raised.")]
    public CustomGameEvent response;

    private void OnEnable() {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data) {
        response.Invoke(sender, data);
    }

}
