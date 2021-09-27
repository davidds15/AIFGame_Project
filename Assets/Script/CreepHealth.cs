using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public int damage = 10;
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(health);
        if (col.gameObject.tag == "Peluru")
        {
            health -= damage;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if(health<=0)
        {
            Destroy(gameObject);
            Destroy(GameObject.FindWithTag("PintuStage1"));
        }
    }
}
