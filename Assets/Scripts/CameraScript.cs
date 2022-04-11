using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playertransform; //player position.
    public Vector3 offset; //How far the camera will stay.

    Vector3 targetPosition;
    Vector3 smoothPosition;

    [SerializeField]
    float lerpconstant;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void LateUpdate()
    {

        targetPosition = playertransform.position + offset; //Future posiution of camera.
        smoothPosition = Vector3.Lerp(transform.position, targetPosition, lerpconstant); //Moving to future position
        transform.position = smoothPosition;
         
    }
}
