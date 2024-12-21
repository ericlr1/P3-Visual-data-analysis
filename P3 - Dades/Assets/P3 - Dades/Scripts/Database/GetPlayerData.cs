using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerData : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    private PlayerData playerData; // Encapsulated player data

    // Start is called before the first frame update
    void Start()
    {
        playerData.timeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerData();
    }

    // Method to update player data
    private void UpdatePlayerData()
    {
        if (player != null)
        {
            playerData.position = player.transform.position;
            playerData.timeElapsed += Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("Player object is not assigned!");
        }
    }

    // Method to get the current player data
    public PlayerData GetCurrentPlayerData()
    {
        return playerData;
    }
}
