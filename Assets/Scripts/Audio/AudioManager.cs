using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio timer")]
    public double goalTime;
    public double musicDuration;

    [Header("Audio Source")]
    public AudioSource musicSource, sfxSource;

    [Header("Audio Source")]
    public AudioClip[] _audioClip;
    public AudioClip BM;
    public AudioClip BM1;
    public AudioClip BM2;
    public AudioClip Boss;
    public AudioClip Hub;

    private int audioToggle = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _audioClip[0] = BM;
        _audioClip[1] = BM1;
        _audioClip[2] = BM2;
        _audioClip[3] = Boss;
        _audioClip[4] = Hub;
    }
    private void Start()
    {
        goalTime = AudioSettings.dspTime + 0.5;
        musicSource.clip = _audioClip[audioToggle];
        musicSource.PlayScheduled(goalTime);

        musicDuration = (double)_audioClip[audioToggle].samples / _audioClip[audioToggle].frequency;
        goalTime = goalTime + musicDuration;
    }
    private void Update()
    {
        if (AudioSettings.dspTime > goalTime - 1)
        {
            PlayScheduledClip();

        }
    }
    private void PlayScheduledClip()
    {
        audioToggle++;
        if (audioToggle < 3)
        {
            musicSource.clip = _audioClip[audioToggle];
            musicSource.PlayScheduled(goalTime);
            musicDuration = (double)_audioClip[audioToggle].samples / _audioClip[audioToggle].frequency;
            goalTime = AudioSettings.dspTime + musicDuration;
        }
        else
        {
            audioToggle = 0;
            return;
        }
    }
    public void SetCurrentClip(AudioClip clip)
    {
        _audioClip[audioToggle] = clip;
    }
}
