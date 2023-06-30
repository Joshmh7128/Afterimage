using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendUIMessage : MonoBehaviour
{
    // sends a UI message when object is enabled
    [SerializeField] float waitTime, endTime;
    [SerializeField] string message;

    private void OnEnable()
    {
        Invoke("SendMessage", waitTime);
        Invoke("SendNull", waitTime + endTime);
    }

    void SendMessage()
    {
        UIHandler.instance.DisplayMessage(message, waitTime + endTime);
    }

    void SendNull()
    {
        UIHandler.instance.DisplayMessage("");
    }
}
