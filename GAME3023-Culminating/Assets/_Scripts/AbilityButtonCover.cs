using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonCover : MonoBehaviour
{
    GameObject player;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<CharacterController>().playerTurn)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
