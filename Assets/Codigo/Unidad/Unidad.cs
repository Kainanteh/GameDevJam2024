using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [SerializeField] private CuadriculaPosicion cuadriculaPosicion;
    private AccionMover accionMover;
    private AccionAtacar accionAtacar;
    private UnidadVidaSistema unidadVidaSistema;

    private enum Direccion
    {
        Norte,
        Sur,
        Este,
        Oeste
    }

    [SerializeField] private Direccion direccionActual = Direccion.Norte;
    private float tiempoTranscurrido;
    private const float intervaloMovimiento = 1f;

    [SerializeField] private bool Moverse;
    [SerializeField] private bool esEnemigo;
    [SerializeField] private bool enMovimiento = false; // Flag para saber si la unidad est� en movimiento
    [SerializeField] private bool enAtaque = false;

    [SerializeField] private bool Nucleo = false;

    private void Awake()
    {

        if (Nucleo == false)
        {

            accionMover = GetComponent<AccionMover>();
            accionAtacar = GetComponent<AccionAtacar>();

        }

        unidadVidaSistema = GetComponent<UnidadVidaSistema>();



    }

    private void Start()
    {

        if (Nucleo == false)
        {

            cuadriculaPosicion = CuadriculaNivel.Instance.GetCuadriculaPosicion(transform.position);
            CuadriculaNivel.Instance.SetUnidadACuadriculaPosicion(cuadriculaPosicion, this);

            AudioGolpeBajo.AudioGolpeBajoEvento += IniciarMovimientoUnidad;
            AudioGolpeBajo.AudioGolpeBajoEvento += IniciarAtaqueUnidad;

       
            accionAtacar.EnMatar += SetEnAtaqueFalse;
        }
    }

    private void Update()
    {
             
        CuadriculaPosicion nuevaCuadriculaPosicion = CuadriculaNivel.Instance.GetCuadriculaPosicion(transform.position);

        if (nuevaCuadriculaPosicion != cuadriculaPosicion)
        {
            CuadriculaPosicion viejaCuadriculaPosicion = cuadriculaPosicion;
            cuadriculaPosicion = nuevaCuadriculaPosicion;
            // La unidad ha cambiado de celda
            CuadriculaNivel.Instance.UnidadSeHaMovidoCuadriculaPosicion(this, viejaCuadriculaPosicion, nuevaCuadriculaPosicion);
        }
             
    }

    private bool HayEnemigoEnDireccion(bool EsEnemigo, Direccion direccion)
    {

        CuadriculaPosicion posicionDestino = cuadriculaPosicion;

        switch (direccion)
        {
            case Direccion.Norte:
                posicionDestino = new CuadriculaPosicion(posicionDestino.x, posicionDestino.z + 1);
                break;
            case Direccion.Sur:
                posicionDestino = new CuadriculaPosicion(posicionDestino.x, posicionDestino.z - 1);
                break;
            case Direccion.Este:
                posicionDestino = new CuadriculaPosicion(posicionDestino.x + 1, posicionDestino.z);
                break;
            case Direccion.Oeste:
                posicionDestino = new CuadriculaPosicion(posicionDestino.x - 1, posicionDestino.z);
                break;
        }
          
        if (accionAtacar.EsValidoAccionCuadriculaPosicion(posicionDestino))
        {
         
            bool unidadEnDestino = CuadriculaNivel.Instance.HayUnidadEnCuadriculaPosicion(posicionDestino);

            if (unidadEnDestino)
            {

                //�?�? Porque
                if (this.esEnemigo)
                {
                    return unidadEnDestino;
                }
                else
                {
                    return unidadEnDestino;
                }

            }

        }

        return false;
    }

    private void IniciarMovimientoUnidad(object sender, EventArgs e)
    {
        if (!enMovimiento && Moverse && !enAtaque) // Verifica que no est� en movimiento y que pueda moverse
        {

            if (this == null)
            {
                return;
            }

       

            StartCoroutine(MoverUnidadConDescanso());
        }
    }

    private IEnumerator MoverUnidadConDescanso()
    {
     
        MoverUnidad(null, null); // Mueve la unidad
        yield return new WaitForSeconds(intervaloMovimiento); // Espera el intervalo de tiempo
        enMovimiento = false; // Marca que la unidad ya no est� en movimiento
       
    }

    private void MoverUnidad(object sender, EventArgs e)
    {
    
        CuadriculaPosicion nuevaPosicion = cuadriculaPosicion;

        switch (direccionActual)
        {
            case Direccion.Norte:
                nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x, cuadriculaPosicion.z + 1);
                break;
            case Direccion.Sur:
                nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x, cuadriculaPosicion.z - 1);
                break;
            case Direccion.Este:
                nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x + 1, cuadriculaPosicion.z);
                break;
            case Direccion.Oeste:
                nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x - 1, cuadriculaPosicion.z);
                break;
        }

        Vector3 nuevaPosicionMundo = CuadriculaNivel.Instance.GetMundoPosicion(nuevaPosicion);

        if (accionMover.EsValidoAccionCuadriculaPosicion(nuevaPosicion))
        {
            enMovimiento = true; // Marca que la unidad est� en movimiento
            accionMover.Mover(nuevaPosicionMundo);
        }

    }

        private void IniciarAtaqueUnidad(object sender, EventArgs e)
        {

            if (this == null)
            {
                return;
            }


            if (!enMovimiento) // Verifica que no est� en movimiento y que pueda moverse
            {
      
                StartCoroutine(AtaqueUnidadConDescanso());

            }

        }

        private IEnumerator AtaqueUnidadConDescanso()
        {
      
            AtacarUnidad(null, null); // Mueve la unidad
            yield return new WaitForSeconds(intervaloMovimiento); // Espera el intervalo de tiempo
            
        }

        private void AtacarUnidad(object sender, EventArgs e)
        {

            if (this == null)
            {
                return;
            }


            CuadriculaPosicion nuevaPosicion = cuadriculaPosicion;

            switch (direccionActual)
            {
                case Direccion.Norte:
                    nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x, cuadriculaPosicion.z + 1);
                    break;
                case Direccion.Sur:
                    nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x, cuadriculaPosicion.z - 1);
                    break;
                case Direccion.Este:
                    nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x + 1, cuadriculaPosicion.z);
                    break;
                case Direccion.Oeste:
                    nuevaPosicion = new CuadriculaPosicion(cuadriculaPosicion.x - 1, cuadriculaPosicion.z);
                    break;
            }

            /* Vector3 nuevaPosicionMundo = CuadriculaNivel.Instance.GetMundoPosicion(nuevaPosicion);*/


            if (HayEnemigoEnDireccion(esEnemigo, direccionActual))
            {

                enAtaque = true;
                Unidad unidadObjetivo = CuadriculaNivel.Instance.GetUnidadACuadriculaPosicion(nuevaPosicion);
  
                accionAtacar.Atacar(unidadObjetivo);
                
            }
            
        }

    public CuadriculaPosicion GetCuadriculaPosicion()
    {
        return cuadriculaPosicion;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public AccionMover GetAccionMover()
    {
        return accionMover;
    }

    public bool EsEnemigo()
    {
        return esEnemigo;
    }

    public void Da�oAVida()
    {
        unidadVidaSistema.Da�o(accionAtacar.GetDa�oAtaque());
    }

    public int GetVidaUnidad()
    {
        return unidadVidaSistema.GetVida();
    }

    public void SetEnAtaqueFalse(object sender, EventArgs e)
    {
        enAtaque = false;
    }
  

}










/*
 public void Damage(int damageAmount)
 {
     healthSystem.Damage(damageAmount);
 }
 private void HealthSystem_OnDead(object sender, EventArgs e)
 {
     LevelGrid.Instance.RemoveUnitAtGridPOsition(gridPosition, this);
     Destroy(gameObject);

     OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
 }

 public float GetHealthNormalized()
 {
     return healthSystem.GetHealthNormalized();
 }*/