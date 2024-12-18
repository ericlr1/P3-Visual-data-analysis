using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerData : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
    }
}
