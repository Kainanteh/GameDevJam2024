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
    [SerializeField] private bool enMovimiento = false; // Flag para saber si la unidad est� en movimiento
    [SerializeField] private bool enAtaque = false;

    public bool NucleoJugador = false;
    public bool NucleoEnemigo = false;

    public event EventHandler empiezaACaminar;
    public event EventHandler paraDeCaminar;

    public event EventHandler ataque;

    public event EventHandler saltarUnidadJugador;

    public event EventHandler recibirUnidadEnemigo;

    public int costePuntosUnidad = 10;

    [SerializeField] private Transform UnidadVisual;

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
          /*  if(esEnemigo == false)
            {*/
                ActualizarRotacion();
          /*  }*/
            
     

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
            paraDeCaminar?.Invoke(this, EventArgs.Empty);
        }

  
    

        if(!HayEnemigoEnDireccion(direccionActual))
        {
            enAtaque = false;
        }



    }

 

    private void ActualizarRotacion()
    {
        if (UnidadVisual == null) { return; }

   

        switch (direccionActual)
        {
            case Direccion.Norte:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direccion.Sur:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direccion.Este:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Direccion.Oeste:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
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

            if (!enMovimiento && HayEnemigoEnDireccion(direccionActual))
            {
                AtacarUnidad();
            }

          
        }



     /*   if (TurnosGolpeBajo.Instance.EsTurnoEnemigo == true && esEnemigo == false && !enMovimiento && !enAtaque)
        {
            saltarUnidadJugador?.Invoke(this, EventArgs.Empty);
        }*/
    }




    private bool HayEnemigoEnDireccion(Direccion direccion)
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

        if (accionAtacar == null) { return false; }

        if (accionAtacar.EsValidoAccionCuadriculaPosicion(posicionDestino))
        {

            bool unidadEnDestino = CuadriculaNivel.Instance.HayUnidadEnCuadriculaPosicion(posicionDestino);



            if (unidadEnDestino)
            {
                Unidad unidadObjetivo = CuadriculaNivel.Instance.GetUnidadACuadriculaPosicion(posicionDestino);

                //�?�? Porque
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

        }

        return false;
    }

    /*    private void IniciarMovimientoUnidad(object sender, EventArgs e)
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
            enMovimiento = true; // Marca que la unidad est� en movimiento
            empiezaACaminar?.Invoke(this, EventArgs.Empty);
            accionMover.Mover(nuevaPosicionMundo);
        }

    }

/*    private void IniciarAtaqueUnidad(object sender, EventArgs e)
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


        if (HayEnemigoEnDireccion(direccionActual))
        {

            enAtaque = true;
            Unidad unidadObjetivo = CuadriculaNivel.Instance.GetUnidadACuadriculaPosicion(nuevaPosicion);

            accionAtacar.Atacar(unidadObjetivo);

            ataque?.Invoke(this, EventArgs.Empty);


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

    public void Da�oAVida(int da�oAtaque)
    {

        unidadVidaSistema.Da�o(da�oAtaque);
        recibirUnidadEnemigo?.Invoke(this, EventArgs.Empty);

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
    public void SetMoverseFalse()
    {
        Moverse = false;
    }

    public void SetDireccion(Direccion nuevaDireccion)
    {
        direccionActual = nuevaDireccion;
    }

    public Direccion GetDireccion()
    {
        return direccionActual;
    }

    public void SetEnMovimiento(bool movimiento)
    {
        enMovimiento = movimiento;
    }

}




