using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    GameObject obj;
    public List<Node> nodes = new List<Node>();
    Rigidbody rb;
    public float moveSpeed = 5;
    Vector3 moveVec = Vector3.zero;

    public string Astar;

    public bool status = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        obj = GameObject.FindGameObjectWithTag(Astar);

    }

    void FixedUpdate()
    {
        nodes = obj.GetComponent<Pathfinding>().FinalPath;
        int currentIdx = 0;
        int someTreshold = 1;
        Node nextPoint = nodes[currentIdx];

        if (status == true)
        {
            transform.position += (nodes[0].vPosition - transform.position).normalized * Time.deltaTime * moveSpeed;

            if (Vector3.Distance(transform.position, nextPoint.vPosition) < someTreshold)
            {  //Next point in list has been reached
                currentIdx += 1;  //Count the index one up
                nextPoint = nodes[currentIdx];  //Get the next Vector of the list and store it
            }
        }
    }


}
