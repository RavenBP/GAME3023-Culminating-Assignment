using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Footsteps : MonoBehaviour
{

    [SerializeField]
    AudioSource footstepSource;

    [SerializeField]
    AudioClip[] footstepClips;

    [SerializeField]
    GameObject player;

    [Header("Tilemap")]
    [SerializeField]
    TileBase grassTile;
    [SerializeField]
    TileBase floorTile;
    [SerializeField]
    TileBase gravelTile;
    [SerializeField]
    TileBase woodTile;
    
    private GameObject tilemapGO;
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }
    
    // Play appropriate sound for specified tile.
    void PlayFootstepSound()
    {
        tilemapGO = GameObject.FindWithTag("BaseTilemap");

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            tilemapGO = GameObject.FindWithTag("BaseTilemap");
        }

        // Get player's position in tilemap cell location
        Vector3 tilemapPositionF = tilemapGO.GetComponent<Tilemap>().WorldToCell(player.transform.position);

        // Convert from Vector3 to Vector3Int
        Vector3Int tilemapPositionI = new Vector3Int(Mathf.FloorToInt(tilemapPositionF.x), Mathf.FloorToInt(tilemapPositionF.y), Mathf.FloorToInt(tilemapPositionF.z));

        if (tilemapGO.GetComponent<Tilemap>().GetTile(tilemapPositionI) == grassTile)
        {
            footstepSource.clip = footstepClips[0];
            footstepSource.Play();

            //Debug.Log("Player is on a grass tile");
        }
        else if (tilemapGO.GetComponent<Tilemap>().GetTile(tilemapPositionI) == floorTile)
        {
            footstepSource.clip = footstepClips[1];
            footstepSource.Play();

            //Debug.Log("Player is on a floor tile");
        }
        else if (tilemapGO.GetComponent<Tilemap>().GetTile(tilemapPositionI) == gravelTile)
        {
            footstepSource.clip = footstepClips[0];
            footstepSource.Play();

            //Debug.Log("Player is on a gravel tile");
        }
        else if (tilemapGO.GetComponent<Tilemap>().GetTile(tilemapPositionI) == woodTile)
        {
            footstepSource.clip = footstepClips[1];
            footstepSource.Play();

            //Debug.Log("Player is on a wood tile");
        }
    }
}
