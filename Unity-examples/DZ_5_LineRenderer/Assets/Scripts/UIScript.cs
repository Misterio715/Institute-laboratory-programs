using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] 
    private Text angleText;
    [SerializeField] 
    private Text angVelocityText;
    [SerializeField] 
    private Rigidbody2D beam;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angleText.text = beam.rotation.ToString();
        angVelocityText.text = Mathf.Abs(beam.angularVelocity).ToString();
    }
}
