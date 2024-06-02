using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionAtacar : MonoBehaviour
{


    private Vector3 posicionObjetivo;
    private Unidad unidad;

    [SerializeField] int da�oAtaque = 10;

    public event EventHandler EnMatar;

    private void Awake()
    {
        unidad = GetComponent<Unidad>();
        posicionObjetivo = transform.position;
    }

    private void Update()
    {

 

    }

    public void Atacar(Unidad unidadObjetivo)
    {

      

        if ((unidadObjetivo.GetVidaUnidad() - da�oAtaque) <= 0)
        {
            EnMatar?.Invoke(this, EventArgs.Empty);
        }

        if(unidadObjetivo.NucleoEnemigo == true || unidadObjetivo.NucleoJugador)
        {
            unidad.Da�oAVida(20);
        }

        unidadObjetivo.Da�oAVida(da�oAtaque);
   /*     if(unidad.EsEnemigo())
        {
            SonidoManager.Instance.ActivarSonido(4);
        }
        else
        {
            SonidoManager.Instance.ActivarSonido(3);
        }*/
      

    }

    public bool EsValidoAccionCuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        if (CuadriculaNivel.Instance.EsValidaCuadriculaPosicion(cuadriculaPosicion)
            && unidad.GetCuadriculaPosicion() != cuadriculaPosicion
            && CuadriculaNivel.Instance.HayUnidadEnCuadriculaPosicion(cuadriculaPosicion))
        {
            return true; 
        }
        else
        {
            return false;
        }

    }

    public int GetDa�oAtaque()
    {
        return da�oAtaque;
    }

}
