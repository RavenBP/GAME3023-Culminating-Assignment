using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObserverBehaviour : MonoBehaviour
{
    public Text encounterText;

    private GameObject player;
    private GameObject enemy;
    private bool turnSwitch = true;
    private string endBattleMessage;
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
            StartCoroutine(EndTurn(2));
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
        player.GetComponent<CharacterController>().isCharged = false;
        player.GetComponent<CharacterController>().isStunned = false;
        player.GetComponent<CharacterController>().playerTurn = true;
        player.GetComponent<CharacterController>().Health = 100;
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

        if (enemy != null)
        {
            enemy.GetComponent<EnemyController>().EncounterDecision();
        }
        player.GetComponent<CharacterController>().SetPlayersTurn(true);
        turnSwitch = true;
    }

    public void PlayerWins(bool victory)
    {
        if (victory)
        {
            StartCoroutine(WinMessage(3));
        }
        else
        {
            StartCoroutine(LossMessage(3));
        }
    }

    IEnumerator WinMessage(float time)
    {
        //Print victory message

        yield return new WaitForSeconds(time);

        player.GetComponent<CharacterController>().Health = 100;
        returnPlayer();
        player.GetComponent<CharacterController>().LoadPlayer();
    }

    IEnumerator LossMessage(float time)
    {
        yield return new WaitForSeconds(time);

        player.GetComponent<CharacterController>().Health = 100;
        returnPlayer();
    }

    public void SetText(string message)
    {
        encounterText.text = message;
    }
}
