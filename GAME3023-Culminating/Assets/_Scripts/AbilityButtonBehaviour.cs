using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonBehaviour : MonoBehaviour
{
    public int abilityID;
    GameObject player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player.GetComponent<CharacterController>().AbilityLockCheck(abilityID))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void OnButtonPressed()
    {
        player.GetComponent<CharacterController>().UseAbility(abilityID);
    }
}
