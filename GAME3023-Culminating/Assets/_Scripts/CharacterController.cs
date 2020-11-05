using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [Header("Character Attributes:")]
    [SerializeField]
    public float MOVEMENT_BASE_SPEED = 3;

    [Space]
    [Header("Character Statistics:")]
    [SerializeField]
    public Vector2 movementDirection;
    [SerializeField]
    public float movementSpeed;

    [Space]
    [Header("References:")]
    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public Animator animator;

    //public bool isPlayersTurn = true;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ProcessInputs();
            Move();
            animator.SetBool("InEncounter", false); // NOTE: Animations may need to be set here if another way cannot be found...
        }
        else if (SceneManager.GetActiveScene().name == "EncounterScene1")
        {
            animator.SetBool("InEncounter", true);
        }
        Animate();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move()
    {
        rb.velocity = (movementDirection * movementSpeed) * MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    public void Ability1()
    {
        Debug.Log("Player used Ability1");
        //isPlayersTurn = false;
    }

    public void Ability2()
    {
        Debug.Log("Player used Ability2");
        //isPlayersTurn = false;
    }

    public void Ability3()
    {
        Debug.Log("Player used Ability3");
        //isPlayersTurn = false;
    }
}
