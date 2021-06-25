using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The AudioManager is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    [SerializeField]
    private AudioSource _effectSource;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _dialogueSource;
    [SerializeField]
    private AudioClip[] _dialogueClips;
    [SerializeField]
    private AudioClip[] _musicClips;

    public void LoopEffect(AudioClip clip)
    {
        
        _effectSource.clip = clip;
        _effectSource.loop = true;
        _effectSource.Play();
    }

    public void StopEffectSource()
    {
        _effectSource.Stop();
    }

    public void PlayEffectSource()
    {
        _effectSource.Play();
    }
    public float GetDialogueTime()
    {
        return _dialogueSource.time;
    }
    public void PlayDialogue(int clipNum, float volume)
    {

        _dialogueSource.Stop();
        _dialogueSource.clip = _dialogueClips[clipNum];
        _dialogueSource.Play();
    }
    public void StopDialogue()
    {
        _dialogueSource.Stop();
    }
    public bool DialogueCheck()
    {
        return _dialogueSource.isPlaying;
    }

    public void PlayEffect(AudioClip effect, float volume)
    {
        _effectSource.PlayOneShot(effect,volume);
    }

    public void EffectLoop(AudioClip effect, float volume, bool playing)
    {
        if (playing == true)
        {
            _effectSource.clip = effect;
            _effectSource.loop = true;
            _effectSource.Play();
        }
        if (playing == false)
        {
            _effectSource.clip = effect;
            _effectSource.loop = false;
            _effectSource.Stop();
        }
    }
    public void StopMusic()
    {
        _musicSource.Stop();
    }
    public void PlayMusic(int MusicClip)
    {
        _musicSource.Stop();
        _musicSource.clip = _musicClips[MusicClip];
        _musicSource.Play();
    }
}
