using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehaviour : MonoBehaviour
{
    GameObject player;

    public void OnResumeButtonPressed()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<CharacterController>().ResumeGame();
    }

    public void OnPauseButtonPressed()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<CharacterController>().PauseGame();
    }
}
