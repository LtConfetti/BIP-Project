using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController characterController;
    public Rigidbody Rigidbody;
    public float gravity = -9.81f;
    public float movementSpeed = 1;

    private bool isFacingRight = true; //Character starts facing right, this is a bool to make sure it IS facing right when D is pressed

    private Vector2 moveInput; 
    private Vector3 velocity;
    private bool isGrounded;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        FlipSprite();

        animator.SetBool("IsWalking", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    private void FlipSprite()
    {
        if (Input.GetKey(KeyCode.A) && isFacingRight)
        {
            Flip();
        }

        else if (Input.GetKey(KeyCode.D) && !isFacingRight)
        {
            Flip();
        }
    }


}
