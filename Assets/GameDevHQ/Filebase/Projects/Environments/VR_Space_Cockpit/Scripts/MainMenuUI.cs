using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class MainMenuUI : MonoBehaviour
{

    [Header("Menus")]

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
  
    [Header("Mixer")]
    [SerializeField] private AudioMixer _mixer;

    [Header("VolumeSliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _SFXSlider, _musicSlider, _dialogueSlider;



    private void Start()
    {
        SetVolumeSliders();
    }

    public void StartGame()
    {

        SceneManager.LoadScene("VR_Cockpit", LoadSceneMode.Single);

       

    }

    private void SetVolumeSliders()
    {
        
        _mixer.GetFloat("masterVolume", out var masterValue );
        _masterSlider.value = masterValue;
        Debug.Log(masterValue);

        _mixer.GetFloat("effectsVolume", out var SFXvalue);
        _SFXSlider.value = SFXvalue;

        _mixer.GetFloat("dialogueVolume", out var dialogueValue);
        _dialogueSlider.value = dialogueValue;

        _mixer.GetFloat("musicVolume", out var musicValue);
        _musicSlider.value = musicValue;




    }

    public void StartCredits()
    {
        SceneManager.LoadScene("EndCredits", LoadSceneMode.Single);
        // Scene scene = SceneManager.GetSceneAt(2);
        //SceneManager.SetActiveScene(scene);
        //Debug.Log("Credits Should Start");
    }

    public void EnableSettingsMenu()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void EnableMainMenu()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void SetMasterVolume()
    {
        _mixer.SetFloat("masterVolume", _masterSlider.value);
        
    }
    public void SetDialogueVolume()
    {
        _mixer.SetFloat("dialogueVolume", _dialogueSlider.value);

    }

    public void SetSFXVolume()
    {
        _mixer.SetFloat("effectsVolume", _SFXSlider.value);
    }

    public void SetMusicVolume()
    {
        _mixer.SetFloat("musicVolume", _musicSlider.value);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
