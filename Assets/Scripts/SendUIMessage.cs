using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendUIMessage : PuzzleElement
{
    // sends a UI message when object is enabled
    [SerializeField] float waitTime, endTime;
    [SerializeField] string message;
    [SerializeField] bool onEnable;

    internal override void Activate()
    {
        Invoke("SendMessage", waitTime);
        Invoke("SendNull", waitTime + endTime);
    }

    private void OnEnable()
    {
        if (onEnable)
        {
            Invoke("SendMessage", waitTime);
            Invoke("SendNull", waitTime + endTime);
        }
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
