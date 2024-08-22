using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Setup
    Transform mainCamera;
    Transform unit;
    Transform worldSpaceCanvas;
    public Vector3 offset;
    
    // Variables
    public float DestroyTime = 3f;
    public float floatUpSpeed = 0.1f;
    private Vector3 upwardsMomentum = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        mainCamera = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = GameObject.FindGameObjectWithTag("mainWorldCanvas").transform;


        transform.SetParent(worldSpaceCanvas);


        // Floating Text Configuration
        Destroy(gameObject, DestroyTime);
        transform.position = unit.position + offset + upwardsMomentum;

    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position); // Look at the camera
        transform.position = unit.position + offset + upwardsMomentum;

        upwardsMomentum.y += floatUpSpeed;

    }

}
