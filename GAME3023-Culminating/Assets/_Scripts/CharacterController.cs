using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour
{
    [SerializeField]

    public float MOVEMENT_BASE_SPEED = 3;
    public bool isAbility3Locked = true;
    public bool isAbility4Locked = true;
    private bool isPlayersTurn = true;
    public bool playerTurn = true;
    public float Health = 100;
    public float slashDamage = 15;
    public float quickStrikeCritChance = 50;
    public float stunChance = 33;
    public bool isCharged = false;
    public bool isStunned = false;
    public GameObject pauseCanvas;
    bool isPaused = false;


    [Space]
    [Header("Character Statistics:")]
    [SerializeField]
    public Vector2 movementDirection;
    [SerializeField]
    public float movementSpeed;
    public int playerScore = 0;

    [Space]
    [Header("References:")]
    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public Animator animator;

    [Space]
    [Header("Abilities:")]
    [SerializeField]
    AudioClip[] audioClips;


    private AudioSource audioSource;
    private GameObject observer;
    private GameObject playerGO; // NOTE: This is used because of the way the player is being handled/instantiated
    private ObserverBehaviour obsScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    public void SavePlayer() // For Menu UI
    {
        playerGO = GameObject.FindWithTag("Player");
        SaveSystem.SavePlayer(playerGO.GetComponent<CharacterController>());

        Debug.Log("Position Saved as: " + playerGO.transform.position);
        Debug.Log("This position: " + playerGO.transform.position);
    }

    public void LoadPlayer() // For Menu UI
    {
        playerGO = GameObject.FindWithTag("Player");
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        playerGO.transform.position = position;

        if (!data.ability3Locked)
        {
            isAbility3Locked = false;
        }
        if (!data.ability4Locked)
        {
            isAbility4Locked = false;
        }

        Debug.Log("Position Loaded as: " + transform.position);
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ProcessInputs();
            Move();
            Animate();
            animator.SetBool("InEncounter", false); // NOTE: Animations may need to be set here if another way cannot be found...

        }
        else if (SceneManager.GetActiveScene().name == "EncounterScene1")
        {
            animator.SetBool("InEncounter", true);
        }
        //Debug.Log(isPlayersTurn);
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move()
    {
        rb.velocity = (movementDirection * movementSpeed) * MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    public void UseAbility(int abilityID)
    {
        observer = GameObject.FindWithTag("CombatObserver");
        obsScript = observer.GetComponent<ObserverBehaviour>();

        if (playerTurn)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy"); //This should probably be moved to ObserverBehaviour
            int randomInt = Random.Range(0, 10);
            if (!isStunned)
            {
                if (abilityID == 1)
                {
                    Debug.Log("Player used Slash");
                    obsScript.SetText("Player used Slash!");

                    audioSource.clip = audioClips[0];
                    audioSource.Play();

                    enemy.GetComponent<EnemyController>().DamageEnemy(slashDamage);
                }
                else if (abilityID == 2)
                {
                    Debug.Log("Player used Quick Strike");
                    obsScript.SetText("Player used Quick Strike!");

                    audioSource.clip = audioClips[1];
                    audioSource.Play();

                    randomInt = Random.Range(0, 100);
                    if (randomInt <= quickStrikeCritChance)
                    {
                        enemy.GetComponent<EnemyController>().DamageEnemy(slashDamage * 2);
                    }
                    else
                    {
                        enemy.GetComponent<EnemyController>().DamageEnemy(slashDamage);
                    }
                }
                else if (abilityID == 3)
                {
                    Debug.Log("Player used Thunderbolt");
                    randomInt = Random.Range(0, 100);
                    obsScript.SetText("Player used Thunderbolt!");

                    audioSource.clip = audioClips[2];
                    audioSource.Play();

                    enemy.GetComponent<EnemyController>().DamageEnemy(slashDamage * 2);
                    if (randomInt <= stunChance)
                    {
                        enemy.GetComponent<EnemyController>().isStunned = true;
                    }
                }
                else if (abilityID == 4)
                {
                    if (!isCharged)
                    {
                        Debug.Log("Player is charging up");
                        obsScript.SetText("Player is charging up...");

                        audioSource.clip = audioClips[3];
                        audioSource.Play();

                        isCharged = true;
                    }
                    else
                    {
                        enemy.GetComponent<EnemyController>().DamageEnemy(enemy.GetComponent<EnemyController>().enemyHealth - 1);

                        audioSource.clip = audioClips[4];
                        audioSource.Play();

                        isCharged = false;
                        Debug.Log("Player lands a massive strike!");
                        obsScript.SetText("Player lands a massive strike!");
                    }
                }
            }
            else
            {
                Debug.Log("You are stunned.");
                obsScript.SetText("Player is stunned.");
                isStunned = false;
            }
            playerTurn = false;
        }
    }

    public bool AbilityLockCheck(int ability)
    {
        if (ability == 3)
        {
            return isAbility3Locked;
        }
        else if (ability == 4)
        {
            return isAbility4Locked;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayersTurn(bool turn)
    {
        playerTurn = turn;
    }

    public void DamagePlayer(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            TriggerDeath();
            Debug.Log("You have died.");
            obsScript.SetText("You have been defeated.");
        }
    }

    void TriggerDeath()
    {
        observer.GetComponent<ObserverBehaviour>().PlayerWins(false);
    }

    public void AwardPlayer(int bonusScore)
    {
        playerScore += bonusScore;

        if (isAbility3Locked)
        {
            isAbility3Locked = false;
        }
        else if (isAbility4Locked)
        {
            isAbility4Locked = false;
        }
    }

    public void PauseGame()
    {
        //pauseCanvas = GameObject.FindWithTag("PauseCanvas");
        isPaused = true;
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        //pauseCanvas = GameObject.FindWithTag("PauseCanvas");
        isPaused = false;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
}
