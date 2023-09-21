using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{

    public int collectorIndex;
    private Vector3 masterPosition;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;


    private Vector3 offsetFromMouse;

    void Start()
    {
        masterPosition = transform.localPosition;
    }


    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.localPosition = new Vector3(mousePos.x-startPosX,mousePos.y-startPosY,0);
        }
        

    }

    private void OnMouseDown() {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            isBeingHeld = true;
        }

    }

    private void OnMouseUp() {
        isBeingHeld = false;
        transform.localPosition = masterPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Collector") && other.gameObject.GetComponent<Collector>().index == collectorIndex)
        {
            masterPosition = other.transform.position;
            transform.localPosition = masterPosition;
            transform.rotation = other.transform.rotation;
            isBeingHeld = false;
        }
    }
}
