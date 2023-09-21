using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLabel : MonoBehaviour
{
    [SerializeField]
    private GameObject label;
    private bool active;

    private void Start()
    {
        active = false;
    }

    public void ToggleVisibility()
    {
        if (active)
        {
            label.SetActive(false);
            active = false;
        }
        else
        {
            label.SetActive(true);
            active = true;
        }
    }

}
