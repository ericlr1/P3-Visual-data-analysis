using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseSend : MonoBehaviour
{
    public GetPlayerData playerData; // Reference to the script or component holding the player's position

    private Vector3 lastSentPosition; // Track the last sent position
    private string serverUrl = "https://citmalumnes.upc.es/~mariogs5/SendPosition.php"; // Replace with your PHP script URL
    private float checkInterval = 0.5f; // Interval in seconds to check for position updates

    // Start is called before the first frame update
    void Start()
    {
        lastSentPosition = playerData.playerPosition;

        // Start the coroutine to send player position
        StartCoroutine(SendPlayerPositionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SendPlayerPositionCoroutine()
    {
        while (true)
        {
            Vector3 currentPosition = playerData.playerPosition;

            // Check if the position has changed
            if (/*currentPosition != lastSentPosition*/ true)
            {
                // Send the new position to the server
                StartCoroutine(SendPositionToServer(currentPosition));

                // Update the last sent position
                lastSentPosition = currentPosition;
            }

            // Wait for the next interval
            yield return new WaitForSeconds(checkInterval);
        }
    }

    IEnumerator SendPositionToServer(Vector3 position)
    {
        // Create the form data
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString("F3", CultureInfo.InvariantCulture)); // Format for 2 decimal places
        form.AddField("y", position.y.ToString("F3", CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString("F3", CultureInfo.InvariantCulture));

        // Send the POST request
        using (UnityWebRequest request = UnityWebRequest.Post(serverUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Position sent successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error sending position: " + request.error);
            }
        }
    }
}
