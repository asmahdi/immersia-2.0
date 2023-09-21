using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllBlocks : MonoBehaviour
{
    private bool[] boxCheck = new bool[4];
    private int count;

    private void Update() {
        count = 0;
        for (int i=0 ; i< boxCheck.Length; i++)
        {
            
            if (boxCheck[i] == true)
            {
                count++;
                //Debug.Log(count);
            }
        }
        if (count == 4)
            {
                Manager.complete = true;
                //Debug.Log(Manager.complete);
                count++;
            }
        
    }


    private void OnTriggerStay2D(Collider2D other) {
        
        
        if (other.gameObject.name == "a_square")
        {
            boxCheck[0] = true;
            //Debug.Log("done");
        
        }

        if (other.gameObject.name == "b_square")
        {
            boxCheck[1] = true;
        
        }

        if (other.gameObject.name == "ab_box-1")
        {
            boxCheck[2] = true;
        
        }

        if (other.gameObject.name == "ab_box-2")
        {
            boxCheck[3] = true;
        
        }

    }
}
