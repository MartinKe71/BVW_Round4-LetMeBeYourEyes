using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    public Vector3 mousePos;
    public Vector3 screenPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;

        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(mousePos), 2f);
    }
}
