#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> SpawnList = new List<GameObject>();

    Vector3 currentPosition;


    private void OnEnable()
    {
        SpawnRandomly();
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != currentPosition)
        {
            currentPosition = transform.position;
            SpawnRandomly();
        }
    }

    void SpawnRandomly()
    {
        if (SpawnList.Count == 0)
            return;

        ClearChildren();

        GameObject spawned = PrefabUtility.InstantiatePrefab(SpawnList[Random.Range(0, SpawnList.Count)]) as GameObject;
        //GameObject spawned = Instantiate(SpawnList[Random.Range(0, SpawnList.Count)]);
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        spawned.transform.parent = transform;
    }

    void ClearChildren()
    {
        Transform[] children = GetChildren(transform);
        if (children.Length == 0)
            return;

        foreach(Transform t in children)
        {
            DestroyImmediate(t.gameObject);
        }
    }

    Transform[] GetChildren(Transform parent)
    {
        var children = new Transform[parent.childCount];

        for (var i = 0; i < children.Length; ++i)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }
}
#endif