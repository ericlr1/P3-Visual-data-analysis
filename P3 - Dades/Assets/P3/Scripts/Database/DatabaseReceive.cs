using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class RetrievePlayerPositions : MonoBehaviour
{
    // URL of the PHP script
    private string url = "https://citmalumnes.upc.es/~mariogs5/RetrievePosition.php";

    // List to store retrieved positions
    public List<Vector3> playerPositions = new List<Vector3>();

    void Start()
    {
        // Start the coroutine to fetch data
        StartCoroutine(FetchPlayerPositions());
    }

    IEnumerator FetchPlayerPositions()
    {
        // Make a GET request to the PHP script
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Deserialize the JSON into a structure
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

                if (responseData.success)
                {
                    // Populate the Vector3 list
                    foreach (var position in responseData.positions)
                    {
                        playerPositions.Add(new Vector3(position.x, position.y, position.z));
                    }

                    Debug.Log("Player positions successfully retrieved!");
                }
                else
                {
                    Debug.LogError("Error retrieving positions: " + responseData.error);
                }
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
            }
        }
    }

    // Classes to deserialize JSON
    [System.Serializable]
    public class ResponseData
    {
        public bool success;
        public List<Position> positions;
        public string error;
    }

    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
}
