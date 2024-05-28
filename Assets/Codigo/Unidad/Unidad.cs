using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [SerializeField] private CuadriculaPosicion cuadriculaPosicion;
    [SerializeField] private List<CuadriculaPosicion> cuadriculaPosicionNucleo;
    private AccionMover accionMover;
    private AccionAtacar accionAtacar;
    private UnidadVidaSistema unidadVidaSistema;

    public enum Direccion
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
    [SerializeField] private bool enMovimiento = false; // Flag para saber si la unidad está en movimiento
    [SerializeField] private bool enAtaque = false;

    public bool NucleoJugador = false;
    public bool NucleoEnemigo = false;

    private void Awake()
    {

        if (NucleoJugador == false && NucleoEnemigo == false)
        {

            accionMover = GetComponent<AccionMover>();
            accionAtacar = GetComponent<AccionAtacar>();

        }

        unidadVidaSistema = GetComponent<UnidadVidaSistema>();



    }

    private void Start()
    {

        if (NucleoJugador == false && NucleoEnemigo == false)
        {

            cuadriculaPosicion = CuadriculaNivel.Instance.GetCuadriculaPosicion(transform.position);
            CuadriculaNivel.Instance.SetUnidadACuadriculaPosicion(cuadriculaPosicion, this);

            AudioGolpeBajo.AudioGolpeBajoEvento += OnAudioGolpeBajoEvento;
   


            accionAtacar.EnMatar += SetEnAtaqueFalse;
        }
        else
        {
            foreach (var cuadricula in cuadriculaPosicionNucleo)
            {
                CuadriculaNivel.Instance.SetUnidadACuadriculaPosicion(cuadricula, this);
            }


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
            enMovimiento = false;
        }

    }

    private void OnAudioGolpeBajoEvento(object sender, EventArgs e)
    {
        if ((TurnosGolpeBajo.Instance.EsTurnoEnemigo && esEnemigo) || (!TurnosGolpeBajo.Instance.EsTurnoEnemigo && !esEnemigo))
        {
            if (!enMovimiento && Moverse && !enAtaque)
            {
                MoverUnidad();
            }

            if (!enMovimiento && HayEnemigoEnDireccion(esEnemigo, direccionActual))
            {
                AtacarUnidad();
            }
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

            Unidad unidadObjetivo = CuadriculaNivel.Instance.GetUnidadACuadriculaPosicion(posicionDestino);

            //¿?¿? Porque
            if (this.esEnemigo && unidadObjetivo.EsEnemigo() == false)
            {
                return true;
            }
            else if (!this.esEnemigo && unidadObjetivo.EsEnemigo() == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        return false;
    }

/*    private void IniciarMovimientoUnidad(object sender, EventArgs e)
    {
        if (!enMovimiento && Moverse && !enAtaque) // Verifica que no esté en movimiento y que pueda moverse
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
        enMovimiento = false; // Marca que la unidad ya no está en movimiento

    }*/

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
            enMovimiento = true; // Marca que la unidad está en movimiento
            accionMover.Mover(nuevaPosicionMundo);
        }

    }

/*    private void IniciarAtaqueUnidad(object sender, EventArgs e)
    {

        if (this == null)
        {
            return;
        }


        if (!enMovimiento) // Verifica que no esté en movimiento y que pueda moverse
        {

            StartCoroutine(AtaqueUnidadConDescanso());

        }

    }

    private IEnumerator AtaqueUnidadConDescanso()
    {

        AtacarUnidad(null, null); // Mueve la unidad
        yield return new WaitForSeconds(intervaloMovimiento); // Espera el intervalo de tiempo

    }*/

    private void AtacarUnidad()
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

    public void DañoAVida(int dañoAtaque)
    {

        unidadVidaSistema.Daño(dañoAtaque);

    }

    public int GetVidaUnidad()
    {
        return unidadVidaSistema.GetVida();
    }

    public void SetEnAtaqueFalse(object sender, EventArgs e)
    {
        enAtaque = false;
    }

    public List<CuadriculaPosicion> GetCuadriculasNucleo()
    {
        return cuadriculaPosicionNucleo;
    }

    public void SetMoverseTrue()
    {
        Moverse = true;
    }

    public void SetDireccion(Direccion nuevaDireccion)
    {
        direccionActual = nuevaDireccion;
    }

    public Direccion GetDireccion()
    {
        return direccionActual;
    }


}




