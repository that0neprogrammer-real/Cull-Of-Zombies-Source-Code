using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private ResolutionManager resolution;
    [SerializeField] private GraphicsManager graphics;
    [SerializeField] private VolumeManager volume;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ExitGame() => Application.Quit();

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("CurrentlyFullscreen", resolution.SaveFullscreen());
        PlayerPrefs.SetInt("CurrentGraphics", graphics.SaveGraphics());

        PlayerPrefs.SetFloat("CurrentMaster", volume.SaveMasterVolume());
        PlayerPrefs.SetFloat("CurrentMusic", volume.SaveMusicVolume());
        PlayerPrefs.SetFloat("CurrentSound", volume.SaveSoundVolume());

        Debug.Log("Settings Saved!");
    }

    public int GetFullscreen() => PlayerPrefs.GetInt("CurrentlyFullscreen", 1);
    public int GetGraphics() => PlayerPrefs.GetInt("CurrentGraphics", 2);
    public float GetMaster() => PlayerPrefs.GetFloat("CurrentMaster", 1f);
    public float GetMusic() => PlayerPrefs.GetFloat("CurrentMusic", 1f);
    public float GetSound() => PlayerPrefs.GetFloat("CurrentSound", 1f);
    public float GetSensivity() => PlayerPrefs.GetFloat("CurrentSensitivity", 0.5f);
}
