using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private int volumeMusic;
    private int vibration;
    [SerializeField] private Text text2;
    [SerializeField] private Text text3;
    [SerializeField] private AudioClip _clickSound;
    
    void Start()
    {
        volumeMusic = PlayerPrefs.GetInt("VolumeMusic", 1);
        UpdateUI();
    }

    public void ChangeVipration()
    {
        AudioManager.getInstance().PlayAudio(_clickSound);
        vibration = vibration == 0 ? 1 : 0;
        PlayerPrefs.SetInt("Vibration", vibration);
        UpdateUI();
    }
    
    public void ChangeMusicVolume()
    {
        AudioManager.getInstance().PlayAudio(_clickSound);
        volumeMusic = volumeMusic == 0 ? 1 : 0;
        PlayerPrefs.SetInt("VolumeMusic", volumeMusic);
        UpdateUI();
    }

    private void UpdateUI()
    {
        text2.text = volumeMusic == 0 ? "MUSIC OFF" : "MUSIC ON";
        text3.text = vibration == 0 ? "VIBRATION OFF" : "VIBRATION ON";
    }
    
}
