using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [SerializeField] private CuadriculaPosicion cuadriculaPosicion;
    private AccionMover accionMover;

    private enum Direccion
    {
        Norte,
        Sur,
        Este,
        Oeste
    }

    [SerializeField] private Direccion direccionActual = Direccion.Norte;
    private float tiempoTranscurrido;
    private const float intervaloMovimiento = 5f;

    [SerializeField] private bool Moverse;

    private void Awake()
    {
        accionMover = GetComponent<AccionMover>();
    }

    private void Start()
    {
        cuadriculaPosicion = CuadriculaNivel.Instance.GetCuadriculaPosicion(transform.position);
        CuadriculaNivel.Instance.SetUnidadACuadriculaPosicion(cuadriculaPosicion, this);

        // Mover a una posición específica al inicio para probar
        /*accionMover.Mover(CuadriculaNivel.Instance.GetMundoPosicion(new CuadriculaPosicion(5, 7)));*/
    }

    private void Update()
    {

      

        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido >= intervaloMovimiento)
        {
            tiempoTranscurrido = 0f;
            if (Moverse)
            {
                
                MoverUnidad();
            }
        }

        CuadriculaPosicion nuevaCuadriculaPosicion = CuadriculaNivel.Instance.GetCuadriculaPosicion(transform.position);

        if (nuevaCuadriculaPosicion != cuadriculaPosicion)
        {
            CuadriculaPosicion viejaCuadriculaPosicion = cuadriculaPosicion;
            cuadriculaPosicion = nuevaCuadriculaPosicion;
            // La unidad ha cambiado de celda
            CuadriculaNivel.Instance.UnidadSeHaMovidoCuadriculaPosicion(this, viejaCuadriculaPosicion, nuevaCuadriculaPosicion);
        }
    }

    private void MoverUnidad()
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
            accionMover.Mover(nuevaPosicionMundo);
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

    /*public bool IsEnemy()
    {
        return isEnemy;
    }

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
}
