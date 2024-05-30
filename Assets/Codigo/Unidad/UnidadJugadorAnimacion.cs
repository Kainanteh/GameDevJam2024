using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class UnidadJugadorAnimacion : MonoBehaviour
{

    public Animator animatorController;

    Unidad unidad;

    private void Awake()
    {
        unidad = GetComponent<Unidad>();
    }

    private void Start()
    {

        unidad.empiezaACaminar += SetCaminando;
        unidad.paraDeCaminar += SetCaminando;

        unidad.ataque += SetAtaque;

    }

    public void SetCaminando(object sender, EventArgs e)
    {
        // Obtener el valor actual del parámetro "caminando"
        bool isCaminando = animatorController.GetBool("caminando");

        // Invertir el valor y establecerlo
        animatorController.SetBool("caminando", !isCaminando);
    }

    public void SetAtaque(object sender, EventArgs e)
    {
        animatorController.SetTrigger("ataque");
    }
}
