using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerData : MonoBehaviour
{
    private GameObject player; // Reference to the player object

    public PlayerData playerData; // Player data

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
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

    public void OnDeath()
    {
        playerData.hasDied = true;
    }

    public void OnJump()
    {
        playerData.hasJumped = true;
    }

}
