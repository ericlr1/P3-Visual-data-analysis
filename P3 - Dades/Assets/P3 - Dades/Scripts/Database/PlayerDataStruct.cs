using UnityEngine;

public struct PlayerData
{
    public Vector3 position;    // Player's current position
    public float timeElapsed;   // Time elapsed since the game started

    public bool hasJumped;
    public bool hasDied;
}