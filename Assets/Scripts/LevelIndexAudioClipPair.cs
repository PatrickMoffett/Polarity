using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelIndexAudioClipPair<TKey, AudioClip>
{
    public LevelIndexAudioClipPair()
    {
    }

    public LevelIndexAudioClipPair(TKey levelIndex, AudioClip value)
    {
        LevelIndex = levelIndex;
        audioClip = value;
    }
    [field: SerializeField] public TKey LevelIndex { set; get; }
    [field: SerializeField] public AudioClip audioClip { set; get; }

}
