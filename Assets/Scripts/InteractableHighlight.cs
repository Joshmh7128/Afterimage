using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    Renderer rend;

    [SerializeField] float speed = 0.5f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        rend.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);
    }
}
