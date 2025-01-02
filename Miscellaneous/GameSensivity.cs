using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameSensivity : MonoBehaviour
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI amount;

    private void Start()
    {
        float value = manager.GetSensivity();

        slider.value = value;
        amount.SetText((value * 10f).ToString("0.0"));
    }

    private void Update() => amount.SetText((slider.value * 10f).ToString("0.0"));

    public void SaveSensitivity() => PlayerPrefs.SetFloat("CurrentSensitivity", slider.value);
}
