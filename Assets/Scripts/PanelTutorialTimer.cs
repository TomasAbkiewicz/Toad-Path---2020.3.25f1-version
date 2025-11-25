using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelTutorialTimer : MonoBehaviour
{
    [Header("Panel con Image")]
    public Image panelImage;

    [Header("Tiempo visible antes de fade")]
    public float displayTime = 4f;

    [Header("Duración del fade")]
    public float fadeDuration = 1f;

    private void Start()
    {
        if (panelImage == null)
        {
            Debug.LogError("No asignaste el Image del panel.");
            return;
        }


        Color c = panelImage.color;
        c.a = 1f;
        panelImage.color = c;

        Invoke(nameof(StartFadeOut), displayTime);
    }

    void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float t = 0f;
        Color startColor = panelImage.color;
        Color endColor = startColor;
        endColor.a = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            panelImage.color = Color.Lerp(startColor, endColor, t / fadeDuration);
            yield return null;
        }

        panelImage.color = endColor;
        panelImage.gameObject.SetActive(false);
    }
}
