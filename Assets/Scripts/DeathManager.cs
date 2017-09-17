using UnityEngine;

class DeathManager: MonoBehaviour
{
    public GameObject heatRayDeath;
    public GameObject normalDeath;
    public static int heatRayDeathCount = 100;
    public static int normalDeathCount = 100;
    private static GameObject[] heatRayDeaths;
    private static GameObject[] normalDeaths;
    private static int heatRayDeathPos = 0;
    private static int normalDeathPos = 0;

    public void Start()
    {
        heatRayDeaths = new GameObject[heatRayDeathCount];
        for (int i = 0; i < heatRayDeathCount; i++) {
            heatRayDeaths[i] = Instantiate(heatRayDeath);
        }

        normalDeaths = new GameObject[normalDeathCount];
        for (int i = 0; i < normalDeathCount; i++) {
            normalDeaths[i] = Instantiate(normalDeath);
        }
    }

    public static GameObject GetHeatRayDeath()
    {
        heatRayDeathPos++;
        if (heatRayDeathPos >= heatRayDeathCount) {
            heatRayDeathPos = 0;
        }

        GameObject heatRayDeath = heatRayDeaths[heatRayDeathPos];

        return heatRayDeath;
    }

    public static GameObject GetNormalDeath()
    {
        normalDeathPos++;
        if (normalDeathPos >= normalDeathCount) {
            normalDeathPos = 0;
        }

        GameObject normalDeath = normalDeaths[normalDeathPos];

        return normalDeath;
    }
}
