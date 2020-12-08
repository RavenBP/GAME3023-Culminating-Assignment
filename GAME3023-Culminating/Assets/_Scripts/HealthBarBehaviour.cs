using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
    GameObject character;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            character = GameObject.FindWithTag("Player");
        }
        else
        {
            character = GameObject.FindWithTag("Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            if (isPlayer)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f * character.GetComponent<CharacterController>().Health, 20);
            }
            else
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f * character.GetComponent<EnemyController>().enemyHealth, 20);
            }
        }
    }
}
