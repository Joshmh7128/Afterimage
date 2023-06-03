#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class WindowRandomizer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    float minOpen = 10f;
    [SerializeField]
    float maxOpen = 80f;

    [SerializeField]
    Transform[] windowFrames = new Transform[6];

    Vector3 currentPosition;


    // Start is called before the first frame update
    void OnEnable()
    {
        RandomizeWindowsRotation();
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != currentPosition)
        {
            currentPosition = transform.position;
            RandomizeWindowsRotation();
        }
    }

    void RandomizeWindowsRotation()
    {
        InitializeRotation();

        for (int i = 0; i < windowFrames.Length; i++)
        {
            if(i == 0 || i == 2)
                windowFrames[i].Rotate(new Vector3(0, Random.Range(minOpen, maxOpen), 0));
            if (i == 1 || i == 3)
                windowFrames[i].Rotate(new Vector3(0, Random.Range(-minOpen, -maxOpen), 0));
            if (i == 4 || i == 5)
                windowFrames[i].Rotate(new Vector3(0, 0, Random.Range(minOpen, maxOpen)));
        }
        //Debug.Log("Randomized");
    }

    void InitializeRotation()
    {
        for (int i = 0; i < windowFrames.Length; i++)
        {
            windowFrames[i].localRotation = Quaternion.EulerRotation(Vector3.zero);
        }
        //Debug.Log("Initialized");
    }
}
#endif
