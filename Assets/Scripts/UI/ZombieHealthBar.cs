using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthBar : MonoBehaviour
{

    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        cam = camera.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.forward);
    }
}
