using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    // Update is called once per frame
    void Update()
    {
        // Vector2 for X, Y movement
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        // Translate character's transform to create movement
        transform.Translate(movementVector);
    }
}
