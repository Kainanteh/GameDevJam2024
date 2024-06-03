using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FundidoAlpha : MonoBehaviour
{
    public Image image; // Asigna esta imagen desde el inspector
    public float duration = 2.0f; // Duración del fundido

    void Start()
    {
        StartCoroutine(FadeToClear());
    }

    IEnumerator FadeToClear()
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            image.color = color;
            yield return null;
        }

        // Asegúrate de que la imagen sea completamente transparente al final
        color.a = 0f;
        image.color = color;
    }
}
