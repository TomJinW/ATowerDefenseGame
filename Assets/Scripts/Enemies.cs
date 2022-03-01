using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Transform[] wayPoints;
    public Transform[] attackPoints;
    public int nextWayPoint = 0;
    public int zombieHealth = 100;
    public float moveSpeed;
    private GameObject finalPosition;
    int index;
    private bool isAttacking = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        transform.position = wayPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(zombieHealth >= 0)
        {
            if(!isAttacking)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, Time.deltaTime * moveSpeed);

                if (Mathf.Abs(Vector3.Distance(transform.position, wayPoints[nextWayPoint].transform.position)) < 0.25f)
                {
                    nextWayPoint++;

                    if (wayPoints.Length <= nextWayPoint)
                    {
                        index = Random.Range(0, attackPoints.Length);
                        isAttacking = true;
                        
                    }
                }
            }
            else
            {
                if (Mathf.Abs(Vector3.Distance(transform.position, attackPoints[index].transform.position)) < 0.25f)
                {
                    animator.SetBool("Attack", true);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, attackPoints[index].transform.position, Time.deltaTime * moveSpeed);
                }
            }
        }
        else if(zombieHealth < -1)
        {
            animator.SetBool("Die", true);
            Destroy(gameObject, 5.0f);
        }
    }
}
