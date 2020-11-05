using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObserverBehaviour : MonoBehaviour
{
    public GameObject player;
    //public GameObject enemy;
    Vector3 oldPlayerLocation;
    public Vector3 newPlayerLocation = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        placePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            returnPlayer();
        }

        //if (player.GetComponent<CharacterController>().isPlayersTurn == false) // Enemy's turn
        //{
        //    enemy.GetComponent<EnemyController>().EncounterDecision();
        //    player.GetComponent<CharacterController>().isPlayersTurn = true;
        //}
    }

    void placePlayer()
    {
        if (player.GetComponentsInChildren<Camera>() != null)
        {
            player.GetComponentsInChildren<AudioListener>()[0].enabled = false;
            player.GetComponentsInChildren<Camera>()[0].enabled = false;
        }

        oldPlayerLocation = player.transform.position;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        player.transform.position = newPlayerLocation;
    }

    public void returnPlayer() // NOTE: This was made public in order for the Flee Button to easily access this function
    {
        player.transform.position = oldPlayerLocation;
        if (player.GetComponentsInChildren<Camera>() != null)
        {
            player.GetComponentsInChildren<AudioListener>()[0].enabled = true;
            player.GetComponentsInChildren<Camera>()[0].enabled = true;
        }
        SceneManager.LoadScene("GameScene");
        //player.GetComponent<CharacterController>().animator.SetBool("InEncounter", false);
        DontDestroyOnLoad(player);
    }
}
