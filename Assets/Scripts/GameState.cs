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
        if((YinPlayer.transform.position - YangPlayer.transform.position).magnitude < WinDistance)
        {
            SceneLoader.Instance.LoadNextScene();
        }
    }
}
