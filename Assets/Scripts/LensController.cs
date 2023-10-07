using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class LensController : MonoBehaviour
{
    public Transform focus;
    public Transform lens;
    public Transform focusSlider;
    public TMP_Text focus_text;
    float fscale;
    

    void Start()
    {
        
    }


    void Update()
    {
        fscale = .4f* (0.5f + focusSlider.localPosition.y);
        focus.localPosition = new Vector3(-0.5f - focusSlider.localPosition.y,focus.localPosition.y, focus.localPosition.z);
        lens.localScale = new Vector3(fscale, 1, 1);
        focus_text.text = "Focal Length = "+System.Math.Round(fscale, 2).ToString();
    }
}
