using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float movementSpeed = 1;

    private bool isFacingRight = true; //Character starts facing right, this is a bool to make sure it IS facing right when D is pressed

    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        //moveInput.Normalize();

        Rigidbody.velocity = new Vector3(moveInput.x * movementSpeed, Rigidbody.velocity.y, moveInput.y * movementSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
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
