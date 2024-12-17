using BasicTowerDefender.Manager;
using UnityEngine;

public class PlayerPrefController : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string Difficulty = "Difficulty";

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MasterVolume, volume);
    }

    public static float GetMasterVolume()
    {
        if (!PlayerPrefs.HasKey(MasterVolume))
        {
            var defaultVolume = AudioManager.DefaultMasterVolume;
            PlayerPrefs.SetFloat(MasterVolume, defaultVolume);
        }

        return PlayerPrefs.GetFloat(MasterVolume);
    }
}