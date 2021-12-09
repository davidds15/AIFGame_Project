using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject Astar;
    public GameObject resetPosition;

    private Animator animator;

    public GameObject target;
    public float satisfactionRadius = 3;
    public float satisfactionDistance = 10;

    public List<Node> nodes = new List<Node>();
    Rigidbody rb;
    public float moveSpeed = 5;
    Vector3 moveVec = Vector3.zero;


    public bool PlayerSeen = false;

    bool statusReset = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();               
        animator = GetComponent<Animator>();
        
    }

    Quaternion lookAtSlowly(Transform t, Vector3 target, float speed)
    {
        Vector3 relativePos = target - t.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);        
        return Quaternion.Lerp(t.rotation, toRotation, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {       
        //Pengechekan jarak antar player
        Vector3 distance = target.transform.position - transform.position;
        if (distance.magnitude <= satisfactionDistance)
        {
            PlayerSeen = true;
        }

        //Pengechekan Animasi Serang
        float satisfactionDistanceForAttack = 1;
        if (distance.magnitude <= satisfactionDistanceForAttack)
        {
            animator.SetBool("Isattacking", true);
        }

        //Pengechekan jarak antar titik Reset
        Vector3 ResetDistance = resetPosition.transform.position - transform.position;
        if (ResetDistance.magnitude >= 20 && statusReset == false)
        {
            Astar.GetComponent<Pathfinding>().targetPos = resetPosition.transform;
            Astar.GetComponent<Grid>().targetPos = resetPosition.transform;

            statusReset = true;
        }
        else if (ResetDistance.magnitude <= 3 && statusReset == true)
        {
            Astar.GetComponent<Pathfinding>().targetPos = GameObject.FindGameObjectWithTag("Player").transform;
            Astar.GetComponent<Grid>().targetPos = GameObject.FindGameObjectWithTag("Player").transform;

            Quaternion target = Quaternion.Euler(0, 180, 0);
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * moveSpeed);

            PlayerSeen = false;
            statusReset = false;
        }


        nodes = Astar.GetComponent<Pathfinding>().finalPath;
        int currentIdx = 0;
        int someTreshold = 1;
        Node nextPoint = nodes[currentIdx];

        if (PlayerSeen == true)
        {
            transform.position += (nodes[0].worldPosition - transform.position).normalized * Time.deltaTime * moveSpeed;

            transform.rotation = lookAtSlowly(transform, nodes[0].worldPosition, 10);

            //set rotation creep jadi 0
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;

            transform.rotation = Quaternion.Euler(eulerAngles);

            if (Vector3.Distance(transform.position, nextPoint.worldPosition) < someTreshold)
            {
                currentIdx += 1;
                nextPoint = nodes[currentIdx];
            }

            animator.SetBool("Iswalking", true);
        }
    }
}
