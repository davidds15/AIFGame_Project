using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public int damage = 20;

    // Timer to track collision time
    float _timeColliding;
    // Time before damage is taken, 1 second default
    public float timeThreshold = 1f;

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Creep")
        {
            // Reset timer
            _timeColliding = 0f;

            Debug.Log("Enemy started colliding with player.");

            // Take damage on impact?
            health -= damage;
        }
    }

    // called each frame the collider is colliding
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Creep")
        {
            // If the time is below the threshold, add the delta time
            if (_timeColliding < timeThreshold)
            {
                _timeColliding += Time.deltaTime;
            }
            else
            {
                // Time is over theshold, player takes damage
                health -= damage; 
                // Reset timer
                _timeColliding = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("SampleScene");

        }
    }
}
