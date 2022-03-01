using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewController : MonoBehaviour
{

    private bool isDragging = false;
    private Vector3 initMousePosition;
    private Vector3 initObjectPosition;

    public void OnStartDragging()
    {
        Debug.Log("Start Dragging");
        initMousePosition = Input.mousePosition;
        initObjectPosition = transform.position;
        isDragging = true;
    }

    public void OnEndDragging()
    {
        isDragging = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            float newX = initObjectPosition.x + (Input.mousePosition.x - initMousePosition.x);
            float newY = initObjectPosition.y + (Input.mousePosition.y - initMousePosition.y);
            float scale = 300 * (Screen.width / 1024.0f);
            transform.position = new Vector2(newX.Bounds(scale, Screen.width - scale), newY.Bounds(scale, Screen.height - scale));
        }
    }
}
