using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float speed = 1 ;
    public float t=0;

    public Color col_a, col_b;

    private void Start() {
        col_a = GetComponent<SpriteRenderer>().color;
        col_b = new Color(col_a.r,col_a.g,col_a.b,0);
    }

    private void Update() {


        if (Manager.complete)
        {   
            
            Fade();
        }
    }

    private void Fade()
    {
        if (t> 1 )
        {
            return;
        }
        GetComponent<SpriteRenderer>().color = Color.Lerp(col_a, col_b,t);
        t += Time.deltaTime * speed;

        
    }
}
