using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public int damage = 20;

    public GameObject txt;
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

        if (collision.gameObject.tag == "Boss")
        {
            // Reset timer
            _timeColliding = 0f;

            Debug.Log("Enemy started colliding with player.");

            // Take damage on impact?
            health -= damage + 10;

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

        if (collision.gameObject.tag == "Boss")
        {
            // If the time is below the threshold, add the delta time
            if (_timeColliding < timeThreshold)
            {
                _timeColliding += Time.deltaTime;
            }
            else
            {
                // Time is over theshold, player takes damage
                health -= damage + 10;
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
            SceneManager.LoadScene("Lose");

        }

        txt.GetComponent<UnityEngine.UI.Text>().text = '\u2665' + health.ToString();
    }
}
