using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    GameObject obj;

    public GameObject target;
    public float satisfactionRadius = 3;
    public float satisfactionDistance = 10;

    public List<Node> nodes = new List<Node>();
    Rigidbody rb;
    public float moveSpeed = 5;
    Vector3 moveVec = Vector3.zero;

    Transform reset;

    public string Astar;

    public bool status = false;
    public bool sensor;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        obj = GameObject.FindGameObjectWithTag(Astar);

        reset = rb.transform;
    }

    Quaternion lookAtSlowly(Transform t, Vector3 target, float speed)
    {
        Vector3 relativePos = target - t.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);        
        return Quaternion.Lerp(t.rotation, toRotation, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        sensor = rb.GetComponent<Sensor>().targetSensed;
        if(sensor == true)
        {
            status = true;
        }


        //Pengechekan jarak antar player
        Vector3 distance = target.transform.position - transform.position;
        if (distance.magnitude <= satisfactionDistance)
        {            
            status = true;
        }

        //Pengechekan jarak antar titik Reset
        Vector3 distance2 = target.transform.position - transform.position;
        if (distance2.magnitude >= 10)
        {
            
        }

        nodes = obj.GetComponent<Pathfinding>().finalPath;
        int currentIdx = 0;
        int someTreshold = 1;
        Node nextPoint = nodes[currentIdx];

        if (status == true)
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
        }
    }
}
