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
    [SerializeField] TextMeshProUGUI timeText, photoText;
    DateTime startTime;

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
        }

        if (request == RequestType.lmb)
        {
            uiElements[0].SetActive(true);
            requestCount = 1;
        }
    }

    private void FixedUpdate()
    {
        requestCount -= Time.fixedDeltaTime;
        if (requestCount <= 0)
            Request(RequestType.none);

        // update our time
        DateTime now = DateTime.Now;
        timeText.text = (now - startTime).ToString();

        // our page count
        photoText.text = "Photos Recovered: " + photoCount.ToString() + " / " + "3";
    }
}
