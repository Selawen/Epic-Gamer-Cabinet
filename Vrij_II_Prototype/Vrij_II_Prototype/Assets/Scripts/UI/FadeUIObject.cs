using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIObject : MonoBehaviour
{
    public Color startColor, endColor;

    private void Start()
    {
        if (GetComponent<Image>()) { GetComponent<Image>().color = startColor; }
        if (GetComponent<Text>()) { GetComponent<Text>().color = startColor; }
    }

    public void Fade(float fadeTime)
    {
        if (GetComponent<Image>()) { StartCoroutine(FadeImage(GetComponent<Image>(), fadeTime)); }
        if (GetComponent<Text>()) { StartCoroutine(FadeText(GetComponent<Text>(), fadeTime)); }
    }

    IEnumerator FadeText(Text text, float ft)
    {
        float t = 0.0f;
        while(t <= 1.0f)
        {
            text.color = Color.Lerp(startColor, endColor, t); 
            t += Time.unscaledDeltaTime / ft;
            yield return null;
            Debug.Log(Time.unscaledTime);
        }
    }

    IEnumerator FadeImage(Image img, float ft)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            img.color = Color.Lerp(startColor, endColor, t); 
            t += Time.unscaledDeltaTime / ft;
            yield return null; 
            Debug.Log(Time.unscaledTime);
        }
    }
}
