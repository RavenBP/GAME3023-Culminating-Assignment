using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Combatent
{
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, 11);

        // Randomly select between animation clips
        if (randomInt >= 0 && randomInt <= 2)
        {
            animator.Play("Golem");
            Debug.Log("Playing Golem Animation Clip");
        }
        else if (randomInt >= 3 && randomInt <= 5)
        {
            animator.Play("Demon");
            Debug.Log("Playing Demon Animation Clip");
        }
        else if (randomInt >= 6 && randomInt <= 8)
        {
            animator.Play("Ogre");
            Debug.Log("Playing Ogre Animation Clip");
        }
        else
        {
            animator.Play("Small Demon");
            Debug.Log("Playing Small Demon Animation Clip");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EncounterDecision()
    {
        int randomInt = Random.Range(0, 10);

        if (randomInt > 5)
        {
            Debug.Log("Enemy used Ability1");
        }
        else
        {
            Debug.Log("Enemy used Ability2");
        }
    }
}
