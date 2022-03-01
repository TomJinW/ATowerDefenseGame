using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float health = 100;
    private float sizeZ;

    // Start is called before the first frame update
    void Start()
    {
        sizeZ = 2 * Camera.main.orthographicSize * (5.0f / 6.0f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sizeZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + (2 * Camera.main.orthographicSize - sizeZ))/ 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            //End game here
            Debug.Log("End Game");
        }
    }
}
