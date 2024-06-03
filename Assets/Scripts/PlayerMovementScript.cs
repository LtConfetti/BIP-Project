using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float movementSpeed = 1;

    private Vector2 moveInput;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        Rigidbody.velocity = new Vector3(moveInput.x * movementSpeed, Rigidbody.velocity.y, moveInput.y * movementSpeed);
    }
}
