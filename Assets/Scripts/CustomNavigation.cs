using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomNavigation : MonoBehaviour
{
    public XRRayInteractor ray;
    private Vector3 targetPos;
    private RaycastHit rayHit;
    void Update()
    {
        if ( ray.GetCurrentRaycastHit(out rayHit))
        {
            targetPos = rayHit.transform.position;
        }

    }

    public void move()
    {
        transform.position = targetPos;
    }
}
