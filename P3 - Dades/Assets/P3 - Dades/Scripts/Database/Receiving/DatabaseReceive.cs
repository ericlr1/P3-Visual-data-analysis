using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;

public class DatabaseReceive : MonoBehaviour
{
    private string serverURL = "https://citmalumnes.upc.es/~mariogs5/";
    private string outputFilePath = "P3 - Dades/DatabaseOutput.json";

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

    [Space(20)]

    public List<Database_PlayerRespawn_View> dbPlayerRespawnsView;
    public List<Database_PlayerPosition_View> dbPlayerPositionsView;
    public List<Database_PlayerJump_View> dbPlayerJumpsView;
    public List<Database_PlayerInteraction_View> dbPlayerInteractionsView;
    public List<Database_PlayerHit_View> dbPlayerHitsView;
    public List<Database_PlayerHeal_View> dbPlayerHealsView;
    public List<Database_PlayerDeath_View> dbPlayerDeathsView;
    public List<Database_PlayerDamaged_View> dbPlayerDamagesView;

    public void Awake()
    {
        ReceiveDataButton();
    }

    public void ReceiveDataButton()
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

        dbPlayerRespawnsView = new List<Database_PlayerRespawn_View>();
        dbPlayerPositionsView = new List<Database_PlayerPosition_View>();
        dbPlayerJumpsView = new List<Database_PlayerJump_View>();
        dbPlayerInteractionsView = new List<Database_PlayerInteraction_View>();
        dbPlayerHitsView = new List<Database_PlayerHit_View>();
        dbPlayerHealsView = new List<Database_PlayerHeal_View>();
        dbPlayerDeathsView = new List<Database_PlayerDeath_View>();
        dbPlayerDamagesView = new List<Database_PlayerDamaged_View>();

        StartCoroutine(FetchAllDatabaseData());
    }

    public IEnumerator ReceiveDataButtonCorrutine()
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

        dbPlayerRespawnsView = new List<Database_PlayerRespawn_View>();
        dbPlayerPositionsView = new List<Database_PlayerPosition_View>();
        dbPlayerJumpsView = new List<Database_PlayerJump_View>();
        dbPlayerInteractionsView = new List<Database_PlayerInteraction_View>();
        dbPlayerHitsView = new List<Database_PlayerHit_View>();
        dbPlayerHealsView = new List<Database_PlayerHeal_View>();
        dbPlayerDeathsView = new List<Database_PlayerDeath_View>();
        dbPlayerDamagesView = new List<Database_PlayerDamaged_View>();

        yield return StartCoroutine(FetchAllDatabaseData());
    }


    private IEnumerator FetchAllDatabaseData()
    {
        // A list of all fetch operations
        List<IEnumerator> fetchOperations = new List<IEnumerator>
        {
            // Tables
            FetchDatabaseData(serverURL + "RetrieveUsers.php", dbUsers),
            FetchDatabaseData(serverURL + "RetrieveSessions.php", dbSessions),
            FetchDatabaseData(serverURL + "RetrievePlayerRespawns.php", dbPlayerRespawns),
            FetchDatabaseData(serverURL + "RetrievePlayerPositions.php", dbPlayerPositions),
            FetchDatabaseData(serverURL + "RetrievePlayerJumps.php", dbPlayerJumps),
            FetchDatabaseData(serverURL + "RetrievePlayerInteractions.php", dbPlayerInteractions),
            FetchDatabaseData(serverURL + "RetrievePlayerHits.php", dbPlayerHits),
            FetchDatabaseData(serverURL + "RetrievePlayerHeals.php", dbPlayerHeals),
            FetchDatabaseData(serverURL + "RetrievePlayerDeaths.php", dbPlayerDeaths),
            FetchDatabaseData(serverURL + "RetrievePlayerDamages.php", dbPlayerDamages),
            
            // Views
            FetchDatabaseData(serverURL + "RetrievePlayerRespawns_View.php", dbPlayerRespawnsView),
            FetchDatabaseData(serverURL + "RetrievePlayerPositions_View.php", dbPlayerPositionsView),
            FetchDatabaseData(serverURL + "RetrievePlayerJumps_View.php", dbPlayerJumpsView),
            FetchDatabaseData(serverURL + "RetrievePlayerInteractions_View.php", dbPlayerInteractionsView),
            FetchDatabaseData(serverURL + "RetrievePlayerHits_View.php", dbPlayerHitsView),
            FetchDatabaseData(serverURL + "RetrievePlayerHeals_View.php", dbPlayerHealsView),
            FetchDatabaseData(serverURL + "RetrievePlayerDeaths_View.php", dbPlayerDeathsView),
            FetchDatabaseData(serverURL + "RetrievePlayerDamages_View.php", dbPlayerDamagesView)
        };

        // Start all fetch operations and wait for them to complete
        foreach (var operation in fetchOperations)
        {
            yield return StartCoroutine(operation);
        }

        // Once all are done, export the data
        ExportDatabaseToFile();
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

    public void ExportDatabaseToFile()
    {
        var exportData = new
        {
            Users = dbUsers,
            Sessions = dbSessions,
            PlayerRespawns = dbPlayerRespawns,
            PlayerPositions = dbPlayerPositions,
            PlayerJumps = dbPlayerJumps,
            PlayerInteractions = dbPlayerInteractions,
            PlayerHits = dbPlayerHits,
            PlayerHeals = dbPlayerHeals,
            PlayerDeaths = dbPlayerDeaths,
            PlayerDamages = dbPlayerDamages
        };

        string jsonOutput = JsonConvert.SerializeObject(exportData, Formatting.Indented);

        try
        {
            string filePath = Path.Combine(Application.dataPath, outputFilePath);
            File.WriteAllText(filePath, jsonOutput);
            Debug.Log($"Database data successfully exported to {filePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to write database data to file: {e.Message}");
        }
    }
}
