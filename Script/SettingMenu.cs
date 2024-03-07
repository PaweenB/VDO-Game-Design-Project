using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private PlayerLook playerLook;

    public void Start()
    {
        Cursor.visible = true;
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Background", Mathf.Log10(volume)*20);
    }

    public void SetMouseSensitivity()
    {
        float sensitivity = mouseSensitivitySlider.value;
        playerLook.xSensitivity = sensitivity;
        playerLook.ySensitivity = sensitivity;
    }
}