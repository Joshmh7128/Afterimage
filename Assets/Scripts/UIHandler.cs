using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    [SerializeField] List<GameObject> uiElements = new List<GameObject>(); // our list of UI elements we want to manipulate
    [SerializeField] TextMeshProUGUI timeText, photoText, messageText;
    DateTime startTime;
    bool canChangeMessage = true;

    float requestCount;

    public int photoCount;

    public void Awake()
    {
        instance = this;
        startTime = DateTime.Now;
    }

    public enum RequestType
    {
        none, lmb
    }

    public void Request(RequestType request)
    {
        if (request == RequestType.none)
        {
            foreach (GameObject go in uiElements) go.SetActive(false);
            if (canChangeMessage)
            DisplayMessage("");
        }

        if (request == RequestType.lmb)
        {
            DisplayMessage("Left Mouse Button to Use");
            requestCount = 1;
        }
    }

    private void FixedUpdate()
    {
        requestCount -= Time.fixedDeltaTime;
        if (requestCount <= 0)
            Request(RequestType.none);

        // update our time
        
        timeText.text = (DateTime.Now - startTime).Hours.ToString("D2") + " : " + (DateTime.Now - startTime).Minutes.ToString("D2") + " : " + (DateTime.Now - startTime).Seconds.ToString("D2");

        // our page count
        photoText.text = "Photos Recovered: " + photoCount.ToString() + " / " + "2";
    }

    public void DisplayMessage(string message)
    {
        messageText.text = message;
    }

    // if we want to stop it from disappearing
    public void DisplayMessage(string message, float overrideTime)
    {
        canChangeMessage = false;
        messageText.text = message;
        Invoke("RunOverride", overrideTime);
    }

    void RunOverride()
    {
        canChangeMessage = true;
        DisplayMessage("");
    }
}
