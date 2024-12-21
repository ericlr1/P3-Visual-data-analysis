using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseSend : MonoBehaviour
{
    // Reference to the script holding the player's data
    public GetPlayerData playerReference;

    private string serverURL = "https://citmalumnes.upc.es/~mariogs5/";
    private int sessionID = -1; // ID of the session in the database

    // Start is called before the first frame update
    void Start()
    {
        StartSession();

        // Start the coroutine to send player position
        StartCoroutine(SendPlayerPositionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        EndSession();
    }

    private IEnumerator SendDataToServer(string url, Dictionary<string, string> data, System.Action<string> callback = null)
    {
        WWWForm form = new WWWForm();

        if (data != null)
        {
            foreach (var entry in data)
            {
                form.AddField(entry.Key, entry.Value);
            }
        }

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(request.downloadHandler.text);
                Debug.Log($"Data sent to the server successfully: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"Error sending data to the server: {request.error}");
            }
        }
    }

    #region PLAYER POSITION

    IEnumerator SendPlayerPositionCoroutine()
    {
        while (true)
        {
            SendPlayerPosition(playerReference.GetCurrentPlayerData());
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SendPlayerPosition(PlayerData playerData)
    {
        string sendPositionURL = serverURL + "SendPosition.php";

        // Prepare data to send
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "x", playerData.position.x.ToString("F3", CultureInfo.InvariantCulture) },
            { "y", playerData.position.y.ToString("F3", CultureInfo.InvariantCulture) },
            { "z", playerData.position.z.ToString("F3", CultureInfo.InvariantCulture) },
            { "time", playerData.timeElapsed.ToString("F3", CultureInfo.InvariantCulture) }
        };

        // Start the coroutine to send the data
        StartCoroutine(SendDataToServer(sendPositionURL, data));
    }

    #endregion

    #region SESSION MANAGEMENT

    private void StartSession()
    {
        string startSessionURL = serverURL + "StartSession.php";

        StartCoroutine(SendDataToServer(startSessionURL, null, (response) =>
        {
            if (int.TryParse(response, out int id))
            {
                sessionID = id;
                Debug.Log($"Session started with ID: {sessionID}");
            }
            else
            {
                Debug.LogError("Failed to parse session ID from response.");
            }
        }));
    }

    private void EndSession()
    {
        if (sessionID == -1)
        {
            Debug.LogWarning("No session to end.");
            return;
        }

        string endSessionURL = serverURL + "EndSession.php";

        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "sessionID", sessionID.ToString() }
        };

        StartCoroutine(SendDataToServer(endSessionURL, data, (response) =>
        {
            Debug.Log($"Session {sessionID} ended: {response}");
        }));
    }

    #endregion

}
