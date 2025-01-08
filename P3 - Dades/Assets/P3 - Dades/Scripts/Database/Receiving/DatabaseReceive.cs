using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;
using System.Linq;

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

        yield return StartCoroutine(FetchAllDatabaseData());
    }


    private IEnumerator FetchAllDatabaseData()
    {
        // A list of all fetch operations
        List<IEnumerator> fetchOperations = new List<IEnumerator>
        {
            FetchDatabaseData(serverURL + "RetrieveUsers.php", dbUsers),
            FetchDatabaseData(serverURL + "RetrieveSessions.php", dbSessions),
            FetchDatabaseData(serverURL + "RetrievePlayerRespawns.php", dbPlayerRespawns),
            FetchDatabaseData(serverURL + "RetrievePlayerPositions.php", dbPlayerPositions),
            FetchDatabaseData(serverURL + "RetrievePlayerJumps.php", dbPlayerJumps),
            FetchDatabaseData(serverURL + "RetrievePlayerInteractions.php", dbPlayerInteractions),
            FetchDatabaseData(serverURL + "RetrievePlayerHits.php", dbPlayerHits),
            FetchDatabaseData(serverURL + "RetrievePlayerHeals.php", dbPlayerHeals),
            FetchDatabaseData(serverURL + "RetrievePlayerDeaths.php", dbPlayerDeaths),
            FetchDatabaseData(serverURL + "RetrievePlayerDamages.php", dbPlayerDamages)
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

    public IEnumerator FetchFilteredDataFromServer<T>(string url, Dictionary<string, string> data) where T : IDatabaseEntity
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, data))
        {
            // Esperamos la respuesta del servidor
            yield return request.SendWebRequest();

            // Comprobamos si la solicitud fue exitosa
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);
                
                // Usamos reflexión para obtener el "responseKey" de la clase T
                var responseKey = typeof(T).GetProperty("responseKey", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)?.GetValue(null) as string;
                //string responseKey = GetResponseKeyForType(dataTypeIndex);

                if (responseKey != null)
                {
                    // Deserializamos la respuesta JSON en la lista adecuada según el tipo T
                    GenericResponse<T> response = JsonConvert.DeserializeObject<GenericResponse<T>>(jsonResponse);

                    if (response.success)
                    {
                        // Verificamos si los datos existen
                        var dataList = response.GetData(responseKey);
                        if (dataList != null && dataList.Length > 0)
                        {
                            // Añadimos los datos a la lista filtrada
                            TileMapManager.tileMap.filteredList.AddRange(dataList);
                            Debug.Log($"Successfully retrieved {dataList.Length} items of type {typeof(T).Name}.");
                        }
                        else
                        {
                            // Si no hay datos, retornamos una lista vacía y mostramos un mensaje
                            TileMapManager.tileMap.filteredList.Clear();
                            Debug.LogWarning($"No items of type {typeof(T).Name} found. Returning an empty list.");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Error retrieving data: {response.error}");
                    }
                }
                else
                {
                    Debug.Log($"ResponseKey not found for type {typeof(T).Name}");
                }
            }
            else
            {
                Debug.LogError("Error retrieving data: " + request.error);
            }
        }
    }

}
