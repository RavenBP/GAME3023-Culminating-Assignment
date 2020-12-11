using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void PlayGame()
    {
        GameObject player;

        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player");

            if (System.IO.File.Exists("player.save"))
            {
                Debug.Log("Save file found!");
                player.GetComponent<CharacterController>().LoadPlayer();
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                Debug.Log("Save file not found!");
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
