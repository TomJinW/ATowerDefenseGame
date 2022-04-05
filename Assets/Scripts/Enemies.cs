using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public Transform[] waypoints;
    public int _currentWaypointIndex = 0;
    public float _speed = 1f;
    [SerializeField] bool isReached = false;
    private Animator animator;
    public float zombieHealth = 100;
    public float zombieMaxHealth = 100;

    public GameManager gameManager = null;

    public int awardMoney = 10;

    public GameObject waypointParent;
    private GameObject path;

    public Slider slider;
    public Image fillImage;

    private void Start()
    {
        zombieHealth *= Internals.zombieHPScaler;
        zombieMaxHealth = zombieHealth;
        if (gameManager == null)
        {
            GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        animator = GetComponentInChildren<Animator>();

        path = waypointParent.transform.GetChild(
            Random.Range(0, waypointParent.transform.childCount)
            ).gameObject;

        waypoints = new Transform[path.transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = path.transform.GetChild(i);
        }

        transform.position = new Vector3(waypoints[0].position.x, transform.position.y, waypoints[0].position.z - .6f);
    }



    private void Update()
    {
        if (slider != null) {
            slider.value = zombieHealth / zombieMaxHealth;
            fillImage.color = Color.Lerp(Color.red, Color.green, slider.value / 1);
        }
        if (zombieHealth <= 0) {
            this.gameObject.SetActive(false);
            gameManager.money += awardMoney;
            Destroy(gameObject);
        }

        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.51f)
        {
            if (_currentWaypointIndex < waypoints.Length - 1)
            {
                _currentWaypointIndex++;
            }
            else
            {
                isReached = true;
                Debug.Log("Final Reached");
                animator.SetBool("Attack", true);
            }
        }
        else
        {
            if (!isReached)
            {
                Debug.DrawLine(transform.position, new Vector3(wp.position.x, transform.position.y, wp.position.z), Color.red);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(wp.position.x, transform.position.y, wp.position.z), _speed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            SphereCollider sc = this.GetComponent<SphereCollider>();
            BoxCollider bc = this.GetComponent<BoxCollider>();
            if (collision.collider != null && sc != null) {
                Physics.IgnoreCollision(collision.collider, sc);
            }
            if (collision.collider != null && bc != null)
            {
                Physics.IgnoreCollision(collision.collider, bc);
            }

        }
        //if (collision.gameObject.tag == "Base")
        //{
        //    Physics.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider>());
        //    Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>());
        //}
        if (collision.gameObject.tag == "Tile")
        {
            Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>());
            //Physics.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider>());
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>());
        }
    }
}
