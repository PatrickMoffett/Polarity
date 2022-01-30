using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public static BackgroundMusic Instance { get { return _instance; } }

    public List<LevelIndexAudioClipPair<int,AudioClip>> AudioMap;

    [SerializeField]
    float AudioFadeOutTime = 2f;

    [SerializeField]
    float AudioFadeInTime = 2f;

    [SerializeField]
    float FadeResolution = .03f;

    AudioSource _audioSource;

    AudioClip pendingAudioClip;
    private void Awake()
    {
        //create this object as a singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_audioSource == null) return;
        int index = scene.buildIndex;
        foreach(LevelIndexAudioClipPair<int, AudioClip> pair in AudioMap)
        {
            if(pair.LevelIndex == index)
            {
                pendingAudioClip = pair.audioClip;
                if (pendingAudioClip == _audioSource.clip)
                {
                    return;
                }

                StartCoroutine(AudioFadeOut(AudioFadeInTime, FadeResolution));
                Invoke("StartNewAudioClip", AudioFadeInTime + .1f);
                return;
            }
        }
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void StartNewAudioClip()
    {
        if(pendingAudioClip == null)
        {
            Debug.LogError("No Pending Audio Clip Set");
            return;
        }
        _audioSource.clip = pendingAudioClip;
        _audioSource.Play();
        StartCoroutine(AudioFadeIn(AudioFadeInTime, FadeResolution));
    }
    
    IEnumerator AudioFadeOut(float FadeTime, float FadeResolution)
    {
        for(float volume = 1f; volume >= 0; volume -= FadeResolution)
        {
            _audioSource.volume = volume;
            yield return new WaitForSeconds(FadeResolution * FadeTime);
        }
    }
    IEnumerator AudioFadeIn(float FadeTime, float FadeResolution)
    {
        for (float volume = 0f; volume <= 1; volume += FadeResolution)
        {
            _audioSource.volume = volume;
            yield return new WaitForSeconds(FadeResolution * FadeTime);
        }
    }
}
