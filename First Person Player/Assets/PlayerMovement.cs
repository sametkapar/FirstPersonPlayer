using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float runSpeed;
    public float stamina = 10f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;


    Vector3 velocity;
    bool isGrounded;
    void Start()
    {
        velocity.y = -2.0f;
    }
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 1)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
            
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        stamina = Mathf.Clamp(stamina, 0f, 100f);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * Time.deltaTime*runSpeed);
            stamina -= 5*Time.deltaTime;
            
        }
         
        if (Input.GetKeyUp(KeyCode.LeftShift) && stamina <= 0)
        {
                controller.Move(move * speed * Time.deltaTime);
                stamina += Time.deltaTime;
            
        }
    }
}
