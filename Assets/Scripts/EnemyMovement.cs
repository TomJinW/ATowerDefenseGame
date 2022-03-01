using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Vector3> movementPoints;
    private Vector3 currentPoint;
    private int currentPointIndex = 0;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = movementPoints[currentPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);

        if(Mathf.Abs(Vector3.Distance(transform.position, currentPoint)) < 0.25f)
        {
            currentPointIndex++;

            if (currentPointIndex == movementPoints.Count)
                currentPointIndex = 0;

            currentPoint = movementPoints[currentPointIndex];
        }
    }
}
