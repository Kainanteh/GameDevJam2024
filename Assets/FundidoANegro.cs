using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FundidoANegro : MonoBehaviour
{
    public Image image; // Asigna esta imagen desde el inspector
    public float duration = 2.0f; // Duración del fundido



    public void FundidoAnegro()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            image.color = color;
            yield return null;
        }

        // Asegúrate de que la imagen sea completamente negra al final
        color.a = 1f;
        image.color = color;

        // Llama a la función OnFadeComplete cuando el fundido termine
        OnFadeComplete();
    }

    void OnFadeComplete()
    {
        // Aquí va la lógica que deseas ejecutar cuando termine el fundido
        SceneManager.LoadScene("EscenaTest1");
        // Puedes realizar cualquier otra acción aquí
    }
}
