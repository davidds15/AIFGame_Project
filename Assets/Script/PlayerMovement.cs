using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody player;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private Animator animator;

    private void Start()
    {
        player = gameObject.AddComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private float rotationSpeed=720;
    void Update()
    {
        float temp = animator.GetFloat("movement");
        animator.SetFloat("movement", temp + (2.0f * Time.deltaTime));
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * playerSpeed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            animator.SetBool("Iswalking",true);
        }
        else
        {
            animator.SetBool("Iswalking", false);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "FinalSpot")
        {
            SceneManager.LoadScene("Win");

        }
    }

    public void Move()
    {
        
    }
}