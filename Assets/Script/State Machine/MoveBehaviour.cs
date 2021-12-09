using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    GameObject player;
    List<Node> nodes;
    int movespeed = 2;

    GameObject Astar;
    GameObject ResetPoint;

    Quaternion lookAtSlowly(Transform t, Vector3 target, float speed)
    {
        Vector3 relativePos = target - t.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        return Quaternion.Lerp(t.rotation, toRotation, speed * Time.deltaTime);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ResetPoint = animator.GetComponent<SelectObject>().resetPoint;
        Astar = animator.GetComponent<SelectObject>().Astar;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nodes = Astar.GetComponent<Pathfinding>().finalPath;
        int currentIdx = 0;
        int someTreshold = 1;
        Node nextPoint = nodes[currentIdx];

        animator.transform.position += (nodes[0].worldPosition - animator.transform.position).normalized * Time.deltaTime * movespeed;

        animator.transform.rotation = lookAtSlowly(animator.transform, nodes[0].worldPosition, 10);

        //set rotation creep jadi 0
        Vector3 eulerAngles = animator.transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        animator.transform.rotation = Quaternion.Euler(eulerAngles);

        if (Vector3.Distance(animator.transform.position, nextPoint.worldPosition) < someTreshold)
        {
            currentIdx += 1;
            nextPoint = nodes[currentIdx];
        }

        Vector3 distance = player.transform.position - animator.transform.position;
        float satisfactionDistance = 3;

        if (distance.magnitude <= satisfactionDistance)
        {
            animator.SetBool("Isattacking", true);
        }

        //RESET
        Vector3 Resetdistance = ResetPoint.transform.position - animator.transform.position;
        float satisfactionDistanceReset = 50;
        if (Resetdistance.magnitude >= satisfactionDistanceReset)
        {
            Astar.GetComponent<Pathfinding>().targetPos = ResetPoint.transform;
            Astar.GetComponent<Grid>().targetPos = ResetPoint.transform;
        }
        else if(Resetdistance.magnitude <= 3)
        {
            Astar.GetComponent<Pathfinding>().targetPos = player.transform;
            Astar.GetComponent<Grid>().targetPos = player.transform;

            Quaternion target = Quaternion.Euler(0, 180, 0);
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, target, Time.deltaTime * movespeed);

            animator.SetBool("Iswalking", false);
        }        

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
