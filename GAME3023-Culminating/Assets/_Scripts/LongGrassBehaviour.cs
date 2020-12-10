using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class LongGrassBehaviour : MonoBehaviour
{
    public GameObject encounterController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (encounterController.tag == "GameController")
            {
                if (encounterController.GetComponent<EncounterObserverBehaviour>() != null)
                {
                    encounterController.GetComponent<EncounterObserverBehaviour>().TryEncounter();
                }
                else
                {
                    Debug.Log("EncounterObserver script is a null reference");
                }
            }
            else
            {
                Debug.Log("Encounter Controller does not have the tag: GameController");
            }
        }
    }
}
