﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterObserverBehaviour : MonoBehaviour
{
    [Header("Tall Grass Attributes:")]
    public float EncounterChance = 10;
    public float CurrentEncounterChance = 0;
    public string EncounterSceneName = "EncounterScene1";
    public GameObject player;
    public bool testEncounterSwitch = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            Instantiate(player, new Vector3(1, -1, 0), Quaternion.identity); // NOTE: I believe that since the player is being instantiated here, the animator is being "lost" whenever the player returns from the EncounterScene

            player = GameObject.FindWithTag("Player");

            LoadLastSave();
        }
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (testEncounterSwitch)
        {
            SceneManager.LoadScene(EncounterSceneName);
            DontDestroyOnLoad(player);
        }
    }

    public void TryEncounter()
    {
        CurrentEncounterChance = Random.Range(0.0f, 100.0f);
        if (CurrentEncounterChance <= EncounterChance)
        {
            //Encounter here
            SceneManager.LoadScene(EncounterSceneName);
            //player.GetComponent<CharacterController>().animator.SetBool("InEncounter", true);
            DontDestroyOnLoad(player);
            //player.GetComponent<CharacterController>().animator.Play("Base Layer.PlayerIdle");
        }
    }

    void LoadLastSave()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        //position.x = data.position[0];
        //position.y = data.position[1];
        //position.z = data.position[2];
        //player.transform.position = position;

        Debug.Log("Position Loaded as: " + transform.position);
    }
}
