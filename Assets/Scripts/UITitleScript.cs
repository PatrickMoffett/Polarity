using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITitleScript : MonoBehaviour
{
    [SerializeField]
    GameObject BackgroundPanel;

    [SerializeField]
    GameObject TitleText;

    [SerializeField]
    public float FadeInSpeed = 2;
    [SerializeField]
    float TimeToShowTitle = 1;
    [SerializeField]
    float FadeOutSpeed = 2;

    [SerializeField]
    float FadeOutAmountPerTick = .01f;

    [SerializeField]
    float FadeInAmountPerTick = .01f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInTitle());

        Invoke("StartFadeOutTitle", FadeInSpeed+TimeToShowTitle);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartFadeOutTitle()
    {
        StartCoroutine(FadeOutTitleAndBackground());
    }
    IEnumerator FadeInTitle()
    {
        Color TextColor = TitleText.GetComponent<TextMeshProUGUI>().color;

        for (float alpha = 0f; alpha <= 1; alpha += FadeInAmountPerTick)
        {
            TextColor.a = alpha;
            TitleText.GetComponent<TextMeshProUGUI>().color = TextColor;
            yield return new WaitForSeconds(.01f * FadeInSpeed);
        }
    }
    public IEnumerator FadeInBackground()
    {
        Color BackgroundColor = BackgroundPanel.GetComponent<Image>().color;

        for (float alpha = 0f; alpha <= 1; alpha += FadeInAmountPerTick)
        {
            BackgroundColor.a = alpha;
            BackgroundPanel.GetComponent<Image>().color = BackgroundColor;
            yield return new WaitForSeconds(.01f * FadeInSpeed);
        }
    }
    IEnumerator FadeOutTitleAndBackground()
    {
        Color BackgroundColor = BackgroundPanel.GetComponent<Image>().color;
        Color TextColor = TitleText.GetComponent<TextMeshProUGUI>().color;

        for (float alpha = 1f; alpha >= 0; alpha -= FadeOutAmountPerTick)
        {
            BackgroundColor.a = alpha;
            TextColor.a = alpha;
            BackgroundPanel.GetComponent<Image>().color = BackgroundColor;
            TitleText.GetComponent<TextMeshProUGUI>().color = TextColor;
            yield return new WaitForSeconds(.01f*FadeOutSpeed);
        }
    }
}
