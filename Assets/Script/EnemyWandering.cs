using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandering : MonoBehaviour
{
    public float moveSpeed = 5;
    Vector3 moveVec = Vector3.zero;
    Rigidbody rb;

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
        moveVec = direction.normalized ;
    }

    void Wandering2()
    {
        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);

        x += lastX;
        y += lastY;
        Vector3 direction = new Vector3(x,0.0f);
        moveVec = direction.normalized*Time.deltaTime;

        lastX = x;
        lastY = y;
    }
}