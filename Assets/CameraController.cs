using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetoffset;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveCamera();
    }

    void moveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position = targetoffset, speed = Time.deltaTime);
    }
}
