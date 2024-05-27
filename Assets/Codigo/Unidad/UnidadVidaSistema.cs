using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadVidaSistema : MonoBehaviour
{

    public event EventHandler EnMuerte;
    public event EventHandler EnDa�o;

    [SerializeField] private int vida = 100;
    private int vidaMaxima;

    private Unidad unidad;

    private void Awake()
    {
        vidaMaxima = vida;
        unidad = GetComponent<Unidad>();
    }


    public void Da�o(int da�oNumero)
    {

        vida -= da�oNumero;

        if (vida < 0)
        {
            vida = 0;
        }

        EnDa�o?.Invoke(this, EventArgs.Empty);

        if (vida == 0)
        {
            Muere();
        }

  /*      Debug.Log(vida);*/

    }

    private void Muere()
    {

        EnMuerte?.Invoke(this, EventArgs.Empty);

        CuadriculaNivel.Instance.LimpiarUnidadACuadriculaPosicion(unidad.GetCuadriculaPosicion());

        Destroy(this.gameObject);

    }

    public float GetVidaNormalizada()
    {

        return (float)vida / vidaMaxima;

    }

    public int GetVida() 
    {
    
        return vida;

    }

}
