using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider[] volumeSliders;

    private void Start()
    {
        volumeSliders[0].value = manager.GetMaster();
        volumeSliders[1].value = manager.GetMusic();
        volumeSliders[2].value = manager.GetSound();
    }

    public void MasterVOlume() => VolumeSetter(volumeSliders[0], "Master");

    public void MusicVolume() => VolumeSetter(volumeSliders[1], "Music");

    public void SFXVolume() => VolumeSetter(volumeSliders[2], "Sound");

    private void VolumeSetter(Slider slider, string mixerParam)
    {
        float amount = slider.value;
        mixer.SetFloat(mixerParam, Mathf.Log10(amount) * 20);
    }

    public float SaveMasterVolume() => volumeSliders[0].value;
    public float SaveMusicVolume() => volumeSliders[1].value;
    public float SaveSoundVolume() => volumeSliders[2].value;
}
