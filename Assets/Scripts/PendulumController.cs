using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PendulumController : MonoBehaviour
{

    public Transform lengthSlider;
    public Transform gravitySlider;
    public float length = 1;
    public float gravity = 9.8f;
    public float limit;
    public TMP_Text length_label;
    public TMP_Text gravity_label;
    public TMP_Text time_label;

    void Start()
    {
        
    }

    
    void Update()
    {
        gravity = 9.8f * (0.5f + gravitySlider.localPosition.y)*2;
        length = 0.5f + lengthSlider.localPosition.y;
        transform.localScale = new Vector3(1, length,1);
        float T = Mathf.Sqrt(gravity/length);

        float angle = limit * Mathf.Cos(Time.time * T);

        length_label.text = "L = " + System.Math.Round(length,2) + " m";
        gravity_label.text = "g = " + System.Math.Round(gravity, 2) + " ms-1";
        time_label.text = "T = " + System.Math.Round(2* Mathf.PI * Mathf.Sqrt( length / gravity), 2) + " s";

        transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}
