using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform Camera;
    public float RotateSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.transform.Rotate(0f, RotateSpeed * Time.deltaTime, 0f);
    }
}
