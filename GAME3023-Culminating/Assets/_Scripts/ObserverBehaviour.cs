using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObserverBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private bool turnSwitch = true;
    Vector3 oldPlayerLocation;
    public Vector3 newPlayerLocation = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        placePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            returnPlayer();
        }

        //player = GameObject.FindWithTag("Player");
        if (!player.GetComponent<CharacterController>().playerTurn && turnSwitch) // Enemy's turn   
        {
            turnSwitch = false;
            StartCoroutine(EndTurn(3));
        }
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

    IEnumerator EndTurn(float time)
    {
        yield return new WaitForSeconds(time);

        enemy.GetComponent<EnemyController>().EncounterDecision();
        player.GetComponent<CharacterController>().SetPlayersTurn(true);
        turnSwitch = true;
    }

    public void PlayerWins(bool victory)
    {
        if (victory)
        {
            player.GetComponent<CharacterController>().Health = 100;
            returnPlayer();
        }
        else
        {
            returnPlayer();
            player.GetComponent<CharacterController>().LoadPlayer();
        }
    }
}
