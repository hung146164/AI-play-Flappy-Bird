using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material mat;
    private Vector2 offset;


    private void Awake()
    {
       mat = GetComponent<Renderer>().material;
    }
    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;

        mat.mainTextureOffset = offset;
    }
    public void SetSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
