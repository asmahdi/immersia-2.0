using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalConstraint : MonoBehaviour
{

    public Rigidbody _rigidbody;
    void Update()
    {
         Vector3 localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
        localVelocity.x = 0;
        localVelocity.z = 0;
                
        _rigidbody.velocity = transform.TransformDirection(localVelocity);
    }
}
