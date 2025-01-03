using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DatabaseReceive : MonoBehaviour
{
    private string serverURL = "https://citmalumnes.upc.es/~mariogs5/";

    public List<Database_User> dbUsers;
    public List<Database_Session> dbSessions;
    public List<Database_PlayerRespawn> dbPlayerRespawns;
    public List<Database_PlayerPosition> dbPlayerPositions;
    public List<Database_PlayerJump> dbPlayerJumps;
    public List<Database_PlayerInteraction> dbPlayerInteractions;
    public List<Database_PlayerHit> dbPlayerHits;
    public List<Database_PlayerHeal> dbPlayerHeals;
    public List<Database_PlayerDeath> dbPlayerDeaths;
    public List<Database_PlayerDamaged> dbPlayerDamages;

    void Awake()
    {
        dbUsers = new List<Database_User>();
        dbSessions = new List<Database_Session>();
        dbPlayerRespawns = new List<Database_PlayerRespawn>();
        dbPlayerPositions = new List<Database_PlayerPosition>();
        dbPlayerJumps = new List<Database_PlayerJump>();
        dbPlayerInteractions = new List<Database_PlayerInteraction>();
        dbPlayerHits = new List<Database_PlayerHit>();
        dbPlayerHeals = new List<Database_PlayerHeal>();
        dbPlayerDeaths = new List<Database_PlayerDeath>();
        dbPlayerDamages = new List<Database_PlayerDamaged>();
    }

    void Start()
    {
        StartCoroutine(FetchDatabaseData(serverURL + "RetrieveUsers.php", dbUsers));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrieveSessions.php", dbSessions));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerRespawns.php", dbPlayerRespawns));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerPositions.php", dbPlayerPositions));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerJumps.php", dbPlayerJumps));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerInteractions.php", dbPlayerInteractions));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerHits.php", dbPlayerHits));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerHeals.php", dbPlayerHeals));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerDeaths.php", dbPlayerDeaths));
        StartCoroutine(FetchDatabaseData(serverURL + "RetrievePlayerDamages.php", dbPlayerDamages));
    }

    public void ReceiveDataButton()
    {
        Awake();
        Start();
    }

    private IEnumerator FetchDatabaseData<T>(string url, List<T> targetList) where T : IDatabaseEntity
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Use reflection to get the static ResponseKey
                var responseKey = typeof(T).GetProperty("responseKey", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)?.GetValue(null) as string;

                if (responseKey != null)
                {
                    // Create a generic container
                    GenericResponse<T> response = JsonConvert.DeserializeObject<GenericResponse<T>>(jsonResponse);

                    if (response.success)
                    {
                        targetList.Clear();
                        targetList.AddRange(response.GetData(responseKey));
                        Debug.Log($"Successfully retrieved {targetList.Count} items of type {typeof(T).Name}.");
                    }
                    else
                    {
                        Debug.LogError($"Error retrieving {typeof(T).Name}: {response.error}");
                    }
                }
                else
                {
                    Debug.LogError($"ResponseKey not found for type {typeof(T).Name}");
                }
            }
            else
            {
                Debug.LogError($"Request failed: {request.error}");
            }
        }
    }

    [System.Serializable]
    public class GenericResponse<T>
    {
        public bool success;
        public string error;

        // Use a dynamic field to deserialize based on the key
        public Dictionary<string, T[]> data;

        public T[] GetData(string key)
        {
            return data != null && data.ContainsKey(key) ? data[key] : new T[0];
        }
    }
}
