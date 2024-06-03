using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public FundidoANegro fundidoANegro;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(QuitAfterDelay(3f)); // Inicia la corrutina con un retraso de 3 segundos
        }
    }

    // Corrutina que espera un tiempo antes de salir del juego
    IEnumerator QuitAfterDelay(float delay)
    {
        fundidoANegro.FundidoAnegro(true);
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        Application.Quit(); // Sale del juego
    }
}
