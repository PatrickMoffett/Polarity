using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    GameObject YinPlayer;
    [SerializeField]
    GameObject YangPlayer;
    [SerializeField]
    float WinDistance = 1f;
    [SerializeField]
    GameObject FusionDance;
    [SerializeField]
    GameObject UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        
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
    private void LoadNextScene()
    {
        SceneLoader.Instance.LoadNextScene();
    }
}
