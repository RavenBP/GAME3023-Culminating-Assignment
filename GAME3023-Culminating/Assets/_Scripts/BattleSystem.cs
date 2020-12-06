using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum BattlePhase
{
    Player,
    Enemy,
    Count
}


public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    BattlePhase phase;

    [SerializeField]
    Combatent[] combatents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AdvanceTurns()
    {
        

        phase++;
        if(phase > BattlePhase.Count)
        {
            phase = 0;
        }

        Debug.Log("It's: " + combatents[(int)phase].gameObject.name + "'s turn!");
        combatents[(int)phase].TakeTurn();
    }
}
