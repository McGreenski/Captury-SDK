using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Transform PointA;
    public Transform PointB;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        PointA = gameObject.transform;
        PointB = gameObject.transform.GetChild(0);
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, PointA.position);
        lineRenderer.SetPosition(1, PointB.position);
    }
}
