using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineController : MonoBehaviour
{
    public Transform target, label;
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0,target.position);
        lineRenderer.SetPosition(1,label.position);
    }
}
