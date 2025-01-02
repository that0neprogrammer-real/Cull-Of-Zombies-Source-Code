using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionsDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private bool isFullscreen;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    private IEnumerator Start()
    {
        InitializeFullscreen();
        yield return null;

        InitializeScreenResolutions();
        yield return null;
    }

    private void InitializeFullscreen()
    {
        bool value = manager.GetFullscreen() == 1; //Get current value from storage

        //Assign value
        fullscreenToggle.isOn = value;
        Screen.fullScreen = value;
        isFullscreen = value;
    }

    private void InitializeScreenResolutions()
    {
        filteredResolutions = new(); //Initialize list of resolutions
        resolutions = Screen.resolutions; //Get all available resolutions

        resolutionsDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate; //Get current refresh rate

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate) 
                filteredResolutions.Add(resolutions[i]); //Only get resolutions with the current refresh rate
        }

        List<string> options = new(); //Initialize new options of resolutions
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x "
                + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";

            options.Add(resolutionOption); //add to the list
            if (filteredResolutions[i].width == Screen.width && 
                filteredResolutions[i].height == Screen.height) currentResolutionIndex = i; //Set current resolution if it matches
                                                                                            //in the list
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue(); //refresh dropdown for the changes
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        isFullscreen = fullscreenToggle.isOn;
    }

    public int SaveFullscreen() => isFullscreen ? 1 : 0;
}
