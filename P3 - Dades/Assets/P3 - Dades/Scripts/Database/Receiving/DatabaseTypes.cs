public interface IDatabaseEntity
{
    // Marker interface for database types
    float x { get; set; }
    float y { get; set; }
    float z { get; set; }

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_User : IDatabaseEntity
{
    public static string responseKey => "users";

    public int userID;
    public string name;
    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string dateOfCreation;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

}

[System.Serializable]
public struct Database_Session : IDatabaseEntity
{
    public static string responseKey => "sessions";

    public int sessionID;
    public int userID;
    public string startTime;
    public string endTime;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerRespawn : IDatabaseEntity
{
    public static string responseKey => "player_respawns";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerPosition : IDatabaseEntity
{
    public static string responseKey => "player_positions";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerJump : IDatabaseEntity
{
    public static string responseKey => "player_jumps";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerInteraction : IDatabaseEntity
{
    public static string responseKey => "player_interactions";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
    public string interactable;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerHit : IDatabaseEntity
{
    public static string responseKey => "player_hits";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
    public string target;
    public int amount;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerHeal : IDatabaseEntity
{
    public static string responseKey => "player_heals";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerDeath : IDatabaseEntity
{
    public static string responseKey => "player_deaths";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

[System.Serializable]
public struct Database_PlayerDamaged : IDatabaseEntity
{
    public static string responseKey => "player_damaged";

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string damager;
    public float damager_x;
    public float damager_y;
    public float damager_z;
    public int amount;

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
}

// ------------------------------------------------------------------------------------------------------- //
// VIEWS 

[System.Serializable]
public struct Database_PlayerRespawn_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_respawns";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
}

[System.Serializable]
public struct Database_PlayerPosition_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_positions";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
}

[System.Serializable]
public struct Database_PlayerJump_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_jumps";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
}

[System.Serializable]
public struct Database_PlayerInteraction_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_interactions";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
    public string interactable;
}

[System.Serializable]
public struct Database_PlayerHit_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_hits";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
    public string target;
    public int amount;
}

[System.Serializable]
public struct Database_PlayerHeal_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_heals";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
}

[System.Serializable]
public struct Database_PlayerDeath_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_deaths";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;
}

[System.Serializable]
public struct Database_PlayerDamaged_View : IDatabaseEntity
{
    public static string responseKey => "users_sessions_damaged";

    public string country { get; set; }
    public int age { get; set; }
    public string gender { get; set; }

    public int sessionID;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float time;

    public string damager;
    public float damager_x;
    public float damager_y;
    public float damager_z;
    public int amount;
}