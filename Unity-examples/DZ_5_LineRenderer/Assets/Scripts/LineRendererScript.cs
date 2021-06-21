using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        for (float i = 0; i < 361; i += 0.5f)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount++, new Vector3(1 * Mathf.Cos(i * Mathf.PI / 180f), 1 * Mathf.Sin(i * Mathf.PI / 180f), 0));
        }

        // CircleCollider2D jointCollider = lineRenderer.gameObject.AddComponent<CircleCollider2D>(); { jointCollider.radius = 1; };
    }

    // Update is called once per frame
    void Update()
    {
    }
}
