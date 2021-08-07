using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 targetoffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float verticle = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, verticle);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        //cam.position = Vector3.Lerp(cam.position, transform.position + targetoffset, speed = Time.deltaTime);
        cam.position = transform.position + targetoffset;
        //cam.Translate(movement * speed * Time.deltaTime, Space.World);
        //Debug.Log(transform.position);
    }

    void Rotate()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
}
