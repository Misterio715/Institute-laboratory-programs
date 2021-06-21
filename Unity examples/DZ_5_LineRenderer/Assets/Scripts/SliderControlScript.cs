using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControlScript : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody2D beamRigidbody;

    public static bool dragging;
    private RaycastHit2D hittedBeam;
    private Vector2 anchor;
    
    // Start is called before the first frame update
    void Start()
    {
        dragging = false;
        anchor = new Vector2(beamRigidbody.transform.GetComponent<HingeJoint2D>().anchor.x,
            beamRigidbody.transform.GetComponent<HingeJoint2D>().anchor.y);
    }

    // Update is called once per frame
    void Update()
    {
        ButtonControl();
        MouseControl();
    }

    private void ButtonControl()
    {
        if (Input.GetKey(KeyCode.A))
        {
            beamRigidbody.angularVelocity += 8f;
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            beamRigidbody.angularVelocity -= 8f;
        }
    }

    private void MouseControl()
    {
        // Debug.Log("dragging: " + dragging);
        
        if (Input.GetMouseButtonDown(0) && HitOnObject() && !dragging)
        {
            hittedBeam.rigidbody.velocity = Vector2.zero;
            hittedBeam.rigidbody.angularVelocity = 0f;
            dragging = true;
            hittedBeam.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            // hittedBeam.rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (dragging && Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dAngle = Mathf.Atan2(anchor.y - mousePosition.y, 
                    anchor.x - mousePosition.x) * 180 / Mathf.PI - hittedBeam.transform.rotation.eulerAngles.z + 180;
            hittedBeam.transform.RotateAround(new Vector3(anchor.x, anchor.y, hittedBeam.transform.position.z),
                Vector3.forward, dAngle);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            // hittedBeam.rigidbody.constraints = RigidbodyConstraints2D.None;
            hittedBeam.rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void AngularControl()
    {
        if (Input.GetMouseButtonDown(0) && HitOnObject() && !dragging)
        {
            hittedBeam.rigidbody.velocity = Vector2.zero;
            hittedBeam.rigidbody.angularVelocity = 0f;
            dragging = true;
            hittedBeam.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            // hittedBeam.rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (dragging && Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dAngle = Mathf.Atan2(anchor.y - mousePosition.y,
                anchor.x - mousePosition.x) * 180 / Mathf.PI - hittedBeam.transform.rotation.eulerAngles.z + 180;
            hittedBeam.rigidbody.angularVelocity += -dAngle / 50;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            // hittedBeam.rigidbody.constraints = RigidbodyConstraints2D.None;
            hittedBeam.rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    bool HitOnObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hittedObject = Physics2D.Raycast(mousePosition, transform.position);
        if (hittedObject.transform.CompareTag("GameController"))
        {
            hittedBeam = hittedObject;
            return true;
        }
        return false;
    }
    
}
