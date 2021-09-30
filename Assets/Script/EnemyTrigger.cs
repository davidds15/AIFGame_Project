using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            obj.GetComponent<EnemyMove>().status = true;

            Destroy(GameObject.FindGameObjectWithTag("Trigger1"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
