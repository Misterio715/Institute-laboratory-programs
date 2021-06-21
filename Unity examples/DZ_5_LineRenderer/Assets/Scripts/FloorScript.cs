using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // [SerializeField] 
    // private Rigidbody2D beam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform.CompareTag("Beam"))
        {
            Debug.Log("Floor trigger");
            SliderControlScript.dragging = false;
            // other.rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
