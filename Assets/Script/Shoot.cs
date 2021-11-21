using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Rigidbody projectile;
    public float speed = 20000;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));

            Destroy(instantiatedProjectile.gameObject, 1);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Creep")
        {
            Destroy(gameObject);
            Debug.Log("Nabrak creeep");
        }
        if (col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Debug.Log("Nabrak wall");
        }
    }
}
