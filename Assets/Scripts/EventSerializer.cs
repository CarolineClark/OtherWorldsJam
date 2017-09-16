using System.Collections;
using UnityEngine;

class EventSerializer
{
    private const string RESPAWN_POINT = "respawnPoint";

    public static Hashtable CreateRespawnPoint(Vector3 respawnPoint)
    {
        Hashtable hash = new Hashtable();
        hash.Add(RESPAWN_POINT, respawnPoint);

        return hash;
    }

    public static Vector3 GetRespawnPoint(Hashtable hash)
    {
        if (hash.ContainsKey(RESPAWN_POINT)) {
            return (Vector3) hash[RESPAWN_POINT];
        }

        return Vector3.zero;
    }
}
