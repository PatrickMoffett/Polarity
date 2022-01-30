using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachSceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button PlayButton = gameObject.GetComponent<Button>();
        PlayButton.onClick.AddListener(delegate { SceneLoader.Instance.LoadScene(1); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
