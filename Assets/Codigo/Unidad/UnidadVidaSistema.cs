using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadVidaSistema : MonoBehaviour
{

    public event EventHandler EnMuerte;
    public event EventHandler EnDaño;

    [SerializeField] private int vida = 100;
    private int vidaMaxima;

    private Unidad unidad;

    private void Awake()
    {
        vidaMaxima = vida;
        unidad = GetComponent<Unidad>();
    }
    

    public void Daño(int dañoNumero)
    {
    
        vida -= dañoNumero;

        if (vida < 0)
        {
            vida = 0;
        }

        EnDaño?.Invoke(this, EventArgs.Empty);

        if (vida == 0)
        {
            Muere();
        }

  

    }

    private void Muere()
    {

        EnMuerte?.Invoke(this, EventArgs.Empty);

        CuadriculaNivel.Instance.LimpiarUnidadACuadriculaPosicion(unidad.GetCuadriculaPosicion());

        if(unidad.NucleoJugador == true || unidad.NucleoEnemigo == true)
        {
            List<CuadriculaPosicion> cuadriculaPosiciones = unidad.GetCuadriculasNucleo();

            foreach (var cuadricula in cuadriculaPosiciones)
            {

                CuadriculaNivel.Instance.LimpiarUnidadACuadriculaPosicion(cuadricula);

            }
            Destroy(this.gameObject);
        }

        /* Destroy(this.gameObject);*/
        unidad.SetEnMovimiento(false);
        unidad.SetMoverseFalse();
        unidad.SetEnAtaqueFalse(this, EventArgs.Empty);



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
