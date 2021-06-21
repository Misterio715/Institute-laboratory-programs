using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D beam;
    [SerializeField]
    private LineRenderer plotAngleLineRenderer;
    [SerializeField]
    private LineRenderer plotAngVelocityLineRenderer;
    [SerializeField]
    private float interval;
    
    private float t;
    private Vector3 coords;
    
    // Start is called before the first frame update
    void Start()
    {
        plotAngleLineRenderer.positionCount = 0;
        plotAngVelocityLineRenderer.positionCount = 0;
        t = 0;
        coords = new Vector3(0, 45, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlotAngle(interval);
        PlotAngVelocity(interval);

        if (t > 80)
        {
            plotAngleLineRenderer.positionCount = 0;
            plotAngVelocityLineRenderer.positionCount = 0;
            t = 0;
        }
    }

    private void PlotAngle(float i)
    {
        float angle = beam.rotation;
        plotAngleLineRenderer.SetPosition(plotAngleLineRenderer.positionCount++, new Vector3(t, angle / 6, 0) + coords);
        t += i;
    }

    private void PlotAngVelocity(float i)
    {
        float angVelocity = Mathf.Abs(beam.angularVelocity);
        plotAngVelocityLineRenderer.SetPosition(plotAngVelocityLineRenderer.positionCount++, new Vector3(t, angVelocity / 2, 0) + coords);
    }
}
