using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Animator animator;

    Vector2 movementVector;

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movementVector * speed * Time.deltaTime);

        animator.SetFloat("Horizontal", movementVector.x);
        animator.SetFloat("Vertical", movementVector.y);
        animator.SetFloat("Speed", movementVector.sqrMagnitude);
    }
}
