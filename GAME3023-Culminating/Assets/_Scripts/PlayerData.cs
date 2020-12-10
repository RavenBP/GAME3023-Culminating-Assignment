using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public bool ability3Locked;
    public bool ability4Locked;

    public PlayerData(CharacterController player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        ability3Locked = player.isAbility3Locked;
        Debug.Log(ability3Locked);
        ability4Locked = player.isAbility4Locked;
    }
}
