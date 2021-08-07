using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChnageColor : MonoBehaviour
{
    //Color current = Renderer.material.color;
    public Renderer mat;
    public Button ready;
    Color def;
    // Start is called before the first frame update
    void Start()
    {
        def = mat.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red")
        {
            mat.material.SetColor("_Color", Color.red);
            ready.image.color = Color.red;
        }
        else if (other.tag == "Blue")
        {
            mat.material.SetColor("_Color", Color.blue);
            ready.image.color = Color.blue;
        }
        else if(other.tag == "Green")
        {
            mat.material.SetColor("_Color", Color.green);
            ready.image.color = Color.green;
        }
        else if(other.tag == "Yellow")
        {
            mat.material.SetColor("_Color", Color.yellow);
            ready.image.color = Color.yellow;
        }
        else
        {
            mat.material.SetColor("_Color", def);
            ready.image.color = def;
        }
    }
}
