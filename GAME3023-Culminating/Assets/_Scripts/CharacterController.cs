using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
<<<<<<< Updated upstream
    float speed = 10;
=======
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

    private GameObject observer;
    private GameObject playerGO; // NOTE: This is used because of the way the player is being handled/instantiated

    void Start()
    {
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
>>>>>>> Stashed changes

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        // Vector2 for X, Y movement
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        // Translate character's transform to create movement
        transform.Translate(movementVector);
=======
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ProcessInputs();
            Move();
            animator.SetBool("InEncounter", false); // NOTE: Animations may need to be set here if another way cannot be found...

        }
        else if (SceneManager.GetActiveScene().name == "EncounterScene1")
        {
            animator.SetBool("InEncounter", true);
        }
        Animate();
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
        if(playerTurn)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy"); //This should probably be moved to ObserverBehaviour
            int randomInt = Random.Range(0, 10);
            if (!isStunned)
            {
                if (abilityID == 1)
                {
                    Debug.Log("Player used Slash");
                    enemy.GetComponent<EnemyController>().DamageEnemy(slashDamage);
                }
                else if (abilityID == 2)
                {
                    Debug.Log("Player used Quick Strike");
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
                        isCharged = true;
                    }
                    else
                    {
                        enemy.GetComponent<EnemyController>().DamageEnemy(enemy.GetComponent<EnemyController>().enemyHealth - 1);
                        isCharged = false;
                        Debug.Log("Player lands a massive strike!");
                    }
                }
            }
            else
            {
                Debug.Log("You are stunned.");
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
>>>>>>> Stashed changes
    }
}
