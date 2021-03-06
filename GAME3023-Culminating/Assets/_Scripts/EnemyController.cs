﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Combatent
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioClip[] audioClips;

    public float enemyHealth = 75;
    public float biteDamage = 15;
    public float lightningDamage = 10;
    public float lightningStunChance = 50;
    public float fleeChance = 75;
    public float struggleChance = 25;
    public int scoreValue = 50;
    public bool isStunned = false;

    private GameObject player;
    private GameObject observer;
    private ObserverBehaviour obsScript;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, 11);
        player = GameObject.FindWithTag("Player");
        observer = GameObject.FindWithTag("CombatObserver");
        obsScript = observer.GetComponent<ObserverBehaviour>();
        audioSource = GetComponent<AudioSource>();
        // Randomly select between animation clips
        if (randomInt >= 0 && randomInt <= 2)
        {
            animator.Play("Golem");
            Debug.Log("Playing Golem Animation Clip");
            obsScript.SetText("A Golem has appeared!");
        }
        else if (randomInt >= 3 && randomInt <= 5)
        {
            animator.Play("Demon");
            Debug.Log("Playing Demon Animation Clip");
            obsScript.SetText("A Demon has appeared!");
        }
        else if (randomInt >= 6 && randomInt <= 8)
        {
            animator.Play("Ogre");
            Debug.Log("Playing Ogre Animation Clip");
            obsScript.SetText("An Ogre has appeared!");
        }
        else
        {
            animator.Play("Small Demon");
            Debug.Log("Playing Small Demon Animation Clip");
            obsScript.SetText("A Lesser Demon has appeared!");
        }
    }

    public void EncounterDecision()
    {
        int randomInt = Random.Range(0, 11);

        if (enemyHealth < 25)
        {
            randomInt = Random.Range(0, 13);
        }

        if(!isStunned)
        {
            if (randomInt <= 4)
            {
                Debug.Log("Enemy used Bite");
                obsScript.SetText("Enemy used Bite!");

                audioSource.clip = audioClips[0];
                audioSource.Play();

                player.GetComponent<CharacterController>().DamagePlayer(biteDamage);
            }
            else if (randomInt == 5 || randomInt == 6 || randomInt == 7)
            {
                Debug.Log("Enemy used Struggle");
                obsScript.SetText("Enemy used Struggle!");

                audioSource.clip = audioClips[2];
                audioSource.Play();

                randomInt = Random.Range(0, 100);

                if (randomInt <= struggleChance)
                {
                    player.GetComponent<CharacterController>().DamagePlayer(player.GetComponent<CharacterController>().Health);
                }
            }
            else if (randomInt == 8 || randomInt == 9)
            {
                Debug.Log("Enemy used Lightning Strike");
                obsScript.SetText("Enemy used Lightning Strike!");

                audioSource.clip = audioClips[1];
                audioSource.Play();

                player.GetComponent<CharacterController>().DamagePlayer(lightningDamage);
                randomInt = Random.Range(0, 100);

                if (randomInt <= lightningStunChance)
                {
                    player.GetComponent<CharacterController>().isStunned = true;
                }
            }
            else if (randomInt >= 10)
            {
                Debug.Log("Enemy has tried to run");
                obsScript.SetText("Enemy tried to run!");
                randomInt = Random.Range(0, 100);

                if (randomInt <= fleeChance)
                {
                    obsScript.SetText("The Enemy has fled the battle");
                    player.GetComponent<CharacterController>().AwardPlayer(scoreValue);
                    observer.GetComponent<ObserverBehaviour>().PlayerWins(true);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                obsScript.SetText("Enemy did nothing");
                Debug.Log("Enemy has made an error.");
            }
        }
        else
        {
            Debug.Log("The Enemy is stunned");
            obsScript.SetText("The Enemy is stunned.");
            isStunned = false;
        }
    }

    public void DamageEnemy(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            TriggerDeath();
        }
    }

    void TriggerDeath()
    {
        Debug.Log("ENEMY DEFEATED");
        obsScript.SetText("The Enemy has been defeated!");
        player.GetComponent<CharacterController>().AwardPlayer(scoreValue);
        observer.GetComponent<ObserverBehaviour>().PlayerWins(true);
        Destroy(this.gameObject);
    }
}
