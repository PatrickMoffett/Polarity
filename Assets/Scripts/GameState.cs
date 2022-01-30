using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState _instance;
    public static GameState Instance { get { return _instance; } }

    [SerializeField]
    GameObject YinPlayer;
    [SerializeField]
    GameObject YangPlayer;
    [SerializeField]
    float WinDistance = .6f;
    [SerializeField]
    GameObject FusionDance;
    [SerializeField]
    GameObject UICanvas;

    Vector3 YinPlayerStart;
    Vector3 YangPlayerStart;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("GameState Instance was already set. Is more than one in the scene?");
        }
        if(YangPlayer == null)
        {
            Debug.LogWarning("Yang Player Reference Not Set On GameState");
        }
        else
        {
            YinPlayerStart = YinPlayer.transform.position;
        }
        if (YinPlayer == null)
        {
            Debug.LogWarning("Yin Player Reference Not Set On GameState");
        }
        else
        {
            YangPlayerStart = YangPlayer.transform.position;
        }
        if (FusionDance == null)
        {
            Debug.LogWarning("Fusion Dance Not Set On GameState");
        }
        if (UICanvas == null)
        {
            Debug.LogWarning("UICanvas Reference Not Set On GameState");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //If Players are null we have already won
        if (YangPlayer == null || YinPlayer == null) return;

        //get a distance vector between the players
        Vector3 DistanceVector = YinPlayer.transform.position - YangPlayer.transform.position;

        //If the distance is less than the win distance Start ending the scene
        if ((DistanceVector).magnitude < WinDistance)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            //destroy the players
            Destroy(YinPlayer);
            Destroy(YangPlayer);

            //show the fusion animation
            Instantiate(FusionDance, (YangPlayer.transform.position + DistanceVector / 2), Quaternion.identity);

            //get the time it will take for the background to fade in
            float BackgroundFadeInTime = UICanvas.GetComponent<UITitleScript>().FadeInSpeed;

            //start fading in the background
            StartCoroutine(UICanvas.GetComponent<UITitleScript>().FadeInBackground());

            //when the background has faded in load next scene
            Invoke("LoadNextScene",BackgroundFadeInTime);
        }
    }
    public void PlayerDied()
    {
        YinPlayer.transform.position = YinPlayerStart;
        YangPlayer.transform.position = YangPlayerStart;
    }
    private void LoadNextScene()
    {
        SceneLoader.Instance.LoadNextScene();
    }
    private void OnDestroy()
    {
        _instance = null;
    }
}
