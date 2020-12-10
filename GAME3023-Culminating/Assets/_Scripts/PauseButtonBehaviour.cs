using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehaviour : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
