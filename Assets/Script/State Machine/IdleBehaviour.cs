using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    GameObject player;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //bool playersensed = animator.GetComponent<Sensor>().targetSensed;

        Vector3 distance = player.transform.position - animator.transform.position;
        float satisfactionDistance = 10;

        if (distance.magnitude <= satisfactionDistance)
        {
            animator.SetBool("Iswalking", true);
        }

        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
