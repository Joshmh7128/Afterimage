using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    [SerializeField] List<GameObject> uiElements = new List<GameObject>(); // our list of UI elements we want to manipulate

    float requestCount;

    public void Awake()
    {
        instance = this;
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
    }
}
