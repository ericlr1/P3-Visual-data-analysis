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

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to send player position
        StartCoroutine(SendPlayerPositionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SendDataToServer(string url, Dictionary<string, string> data)
    {
        WWWForm form = new WWWForm();

        foreach (var entry in data)
        {
            form.AddField(entry.Key, entry.Value);
        }

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
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
}
