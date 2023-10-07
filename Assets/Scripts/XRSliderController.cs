using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSliderController : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public Transform btn;
    private bool interactable = false;
    RaycastHit hit;

    float buttonTempY;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (interactable )
        {
            rayInteractor.GetCurrentRaycastHit(out hit);
            float offset = hit.point.y; 
            
            btn.position = new Vector3(btn.position.x, offset, btn.position.z);
            
            
        }
    }

    public void SelectButton()
    {
        interactable = true;
        buttonTempY = btn.position.y;
    }

    public void DeselectButton()
    {
        interactable = false;
        
    }


   
}
