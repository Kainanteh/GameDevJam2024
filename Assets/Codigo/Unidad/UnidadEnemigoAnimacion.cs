using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadEnemigoAnimacion : MonoBehaviour
{

    public Animator animatorController;

    Unidad unidad;
    UnidadVidaSistema unidadVidaSistema;

    private void Awake()
    {
        unidad = GetComponent<Unidad>();
        unidadVidaSistema = unidad.GetComponent<UnidadVidaSistema>();
    }


    private void Start()
    {
        unidad.ataque += SetAtaque;
        unidad.recibirUnidadEnemigo += SetRecibir;
        unidadVidaSistema.EnMuerte += SetMorir;
    }

    public void SetAtaque(object sender, EventArgs e)
    {
        animatorController.SetTrigger("ataque");
    }

    public void SetRecibir(object sender, EventArgs e)
    {
        animatorController.SetTrigger("recibir");
    }

    public void SetMorir(object sender, EventArgs e)
    {
        animatorController.SetTrigger("morir");
    }

    public void SetBailar(object sender, EventArgs e)
    {
        animatorController.SetTrigger("bailar");
    }

    /*  private void Start()
      {

          unidad.empiezaACaminar += SetCaminando;
          unidad.paraDeCaminar += SetCaminando;

          unidad.ataque += SetAtaque;
          unidad.saltarUnidadJugador += SetSalto;

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

      public void SetSalto(object sender, EventArgs e)
      {
          animatorController.SetTrigger("saltoUnidadJugador");

      }*/


}
