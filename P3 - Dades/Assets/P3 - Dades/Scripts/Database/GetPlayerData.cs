using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

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

    public void OnRespawn()
    {
        playerData.hasRespawned = true;
    }

    public void OnDamaged(string damager, Vector3 source, int amount)
    {
        playerData.hasReceivedDamage = true;

        playerData.damager = damager;
        playerData.damageSource = source;
        playerData.damageAmount = amount;
    }

    public void OnHeal()
    {
        playerData.hasHealed = true;
    }

    public void OnPlayerHit(string target, int damageDealt)
    {
        playerData.hasHit = true;

        playerData.target = target;
        playerData.damageDealt = damageDealt;
    }

    public void OnPlayerInteraction(string interactable)
    {
        playerData.hasInteracted = true;
        playerData.interactable = interactable;
    }
}
