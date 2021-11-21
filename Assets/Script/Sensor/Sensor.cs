using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public Transform targetGO;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool targetSensed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SensoryRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SensoryRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            Sensorcheck();
        }
    }

    private void Sensorcheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            targetGO = rangeChecks[0].transform;
            Vector3 directionTotarget = (targetGO.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTotarget) < angle / 2)
            {
                float distanceTotarget = Vector3.Distance(transform.position, targetGO.position);

                if (!Physics.Raycast(transform.position, directionTotarget, distanceTotarget, obstacleMask))
                {
                    targetSensed = true;
                }
                else
                {
                    targetSensed = false;
                }
            }
            else
            {
                targetSensed = false;
            }
        }
        else if (targetSensed)
        {
            targetSensed = false;
        }
    }
}
