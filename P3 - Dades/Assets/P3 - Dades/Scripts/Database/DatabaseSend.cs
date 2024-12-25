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

    private float jumpCooldown = 0.5f; // Cooldown time in seconds
    private float deathCooldown = 0.5f;

    private float lastJumpSendTime = -Mathf.Infinity; // Time when the last jump position was sent
    private float lastDeathSendTime = -Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        // Creates a new user and starts a new session.
        // Then it starts tracking the position of the player and the game time elapsed.
        CreateUser();
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;

        // Jump Management
        if (playerReference.playerData.hasJumped)
        {
            // Check cooldown and send data if allowed
            if (currentTime >= lastJumpSendTime + jumpCooldown)
            {
                SendJumpPosition(playerReference.playerData);
                lastJumpSendTime = currentTime; // Update the last send time
            }

            // Reset the boolean immediately
            playerReference.playerData.hasJumped = false;
        }

        // Death Management
        if (playerReference.playerData.hasDied)
        {
            // Check cooldown and send data if allowed
            if (currentTime >= lastDeathSendTime + deathCooldown)
            {
                SendDeathPosition(playerReference.playerData);

                lastDeathSendTime = currentTime; // Update the last send time
            }

            // Reset the boolean immediately
            playerReference.playerData.hasDied = false;
        }
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

    #region USER CREATION

    public void CreateUser()
    {
        string createUserURL = serverURL + "CreateUser.php";

        User user = User.CreateNewUser();

        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "name", user.name },
            { "country", user.country },
            { "age", user.age.ToString() },
            { "gender", user.gender }
        };

        StartCoroutine(SendDataToServer(createUserURL, data, (response) =>
        {
            if (int.TryParse(response, out int id))
            {
                user.userID = id;
                Debug.Log($"User created with ID: {user.userID}");

                StartSession(user.userID);
            }
            else
            {
                Debug.LogError("Failed to parse user ID from response.");
            }
        }));
    }

    #endregion

    #region SESSION MANAGEMENT

    private void StartSession(int userID)
    {
        if (userID == -1)
        {
            Debug.LogError("Cannot start session. UserID is invalid.");
            return;
        }

        string startSessionURL = serverURL + "StartSession.php";

        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "userID", userID.ToString() }
        };

        StartCoroutine(SendDataToServer(startSessionURL, data, (response) =>
        {
            if (int.TryParse(response, out int id))
            {
                sessionID = id;
                Debug.Log($"Session started with ID: {sessionID}");

                // Start the coroutine to send player position
                StartCoroutine(SendPlayerPositionCoroutine());
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

    #region PLAYER POSITION

    IEnumerator SendPlayerPositionCoroutine()
    {
        // Wait until the sessionID is valid (sessionID > 0)
        while (sessionID == -1)
        {
            yield return null;
        }

        while (true)
        {
            SendPlayerPosition(playerReference.playerData);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SendPlayerPosition(PlayerData playerData)
    {
        string sendPositionURL = serverURL + "SendPosition.php";

        // Prepare data to send
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "sessionID", sessionID.ToString() },
            { "x", playerData.position.x.ToString("F3", CultureInfo.InvariantCulture) },
            { "y", playerData.position.y.ToString("F3", CultureInfo.InvariantCulture) },
            { "z", playerData.position.z.ToString("F3", CultureInfo.InvariantCulture) },
            { "time", playerData.timeElapsed.ToString("F3", CultureInfo.InvariantCulture) }
        };

        // Start the coroutine to send the data
        StartCoroutine(SendDataToServer(sendPositionURL, data));
    }

    #endregion

    #region PLAYER DEATH

    private void SendDeathPosition(PlayerData playerData)
    {
        string sendDeathURL = serverURL + "SendDeath.php";

        // Prepare data to send
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "sessionID", sessionID.ToString() },
            { "x", playerData.position.x.ToString("F3", CultureInfo.InvariantCulture) },
            { "y", playerData.position.y.ToString("F3", CultureInfo.InvariantCulture) },
            { "z", playerData.position.z.ToString("F3", CultureInfo.InvariantCulture) },
            { "time", playerData.timeElapsed.ToString("F3", CultureInfo.InvariantCulture) }
        };

        // Start the coroutine to send the data
        StartCoroutine(SendDataToServer(sendDeathURL, data));
    }

    #endregion

    #region PLAYER JUMP

    private void SendJumpPosition(PlayerData playerData)
    {
        string sendDeathURL = serverURL + "SendJump.php";

        // Prepare data to send
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "sessionID", sessionID.ToString() },
            { "x", playerData.position.x.ToString("F3", CultureInfo.InvariantCulture) },
            { "y", playerData.position.y.ToString("F3", CultureInfo.InvariantCulture) },
            { "z", playerData.position.z.ToString("F3", CultureInfo.InvariantCulture) },
            { "time", playerData.timeElapsed.ToString("F3", CultureInfo.InvariantCulture) }
        };

        // Start the coroutine to send the data
        StartCoroutine(SendDataToServer(sendDeathURL, data));
    }

    #endregion

}
