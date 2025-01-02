using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    private int currentQuality;

    private void Start()
    {
        graphicsDropdown.ClearOptions();
        currentQuality = manager.GetGraphics();
        QualitySettings.SetQualityLevel(currentQuality);

        string[] qualityNames = QualitySettings.names;
        graphicsDropdown.AddOptions(new List<string>(qualityNames));
        graphicsDropdown.value = currentQuality;
    }

    public void SetGraphics()
    {
        int selectedQualityIndex = graphicsDropdown.value; // Get the index of the selected quality level from the dropdown
        QualitySettings.SetQualityLevel(selectedQualityIndex); // Set the graphics quality to the selected level
        currentQuality = selectedQualityIndex;
    }

    public int SaveGraphics() => currentQuality;
}
