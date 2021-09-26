using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandering : MonoBehaviour
{
    public float moveSpeed = 5;
    Vector3 moveVec = Vector3.zero;
    Rigidbody rb;

    public GameObject target;
    public float satisfactionRadius = 3;
    public float satisfactionDistance = 10;

    public float wanderCooldown = 1;
    public float lastX = 0;
    public float lastY = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        wanderCooldown -= Time.deltaTime;

        if (wanderCooldown <= 0)
        {            
            Wandering();
            wanderCooldown = 1;
        }

        rb.velocity = moveVec * moveSpeed;
    }

    void Wandering()
    {
        float x = Random.Range(-1, 2);
        float y = Random.Range(-1, 2);
        Vector3 direction = new Vector3(x,0.0f,0.0f);

        Vector3 distance = target.transform.position - transform.position;

        if(distance.magnitude <= satisfactionDistance)
        {
            if (distance.magnitude < satisfactionRadius)
            {
                distance = Vector3.zero;
            }
            moveVec = distance.normalized;
        }
        else
        {
            moveVec = direction.normalized;
        }
    }

    void Wandering2()
    {
        Vector3 direction = target.transform.position - transform.position;

        if (direction.magnitude < satisfactionDistance)
        {
            direction = Vector2.zero;
        }
        moveVec = direction.normalized;
    }
}