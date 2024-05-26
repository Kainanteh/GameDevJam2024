using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionMover : MonoBehaviour
{

    private Vector3 posicionObjetivo;
    private Unidad unidad;

     
    private void Awake()
    {
        unidad = GetComponent<Unidad>();
        posicionObjetivo = transform.position;
    }

    private void Update()
    {
        
        float distanciaParada = .1f;

        if(Vector3.Distance(transform.position, posicionObjetivo) > distanciaParada)
        {

            Vector3 movimientoDireccion = (posicionObjetivo - transform.position).normalized;
            float movimientoVelocidad = 4f;
            transform.position += movimientoDireccion * movimientoVelocidad * Time.deltaTime;

            float rotacionVelocidad = 10f;
            transform.forward = Vector3.Lerp(transform.forward, movimientoDireccion, Time.deltaTime * rotacionVelocidad);

      

            // Animacion bool isWalking true


        }
        else
        {
            // Animacion bool isWalking false
        }

    }

    public void Mover(Vector3 posicionObjetivo)
    {
        this.posicionObjetivo = posicionObjetivo;
    }

    public bool EsValidoAccionCuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        List<CuadriculaPosicion> validoCuadriculoPosicionLista = GetValidoAccionCuadriculaPosicionLista();
        return validoCuadriculoPosicionLista.Contains(cuadriculaPosicion);

    }

    public List<CuadriculaPosicion> GetValidoAccionCuadriculaPosicionLista()
    {
        List<CuadriculaPosicion> validoCuadriculoPosicionLista = new List<CuadriculaPosicion>();

        CuadriculaPosicion unidadCuadriculaPosicion = unidad.GetCuadriculaPosicion();

        for (int x = -1; x < CuadriculaNivel.Instance.GetAlto()-1; x++)
        {

            for (int z = -1; z < CuadriculaNivel.Instance.GetAncho()-1; z++)
            {

                CuadriculaPosicion temporalCuadriculaPosicion = new CuadriculaPosicion(x,z);
                CuadriculaPosicion testarCuadriculaPosicion = unidadCuadriculaPosicion + temporalCuadriculaPosicion;




                if (!CuadriculaNivel.Instance.EsValidaCuadriculaPosicion(testarCuadriculaPosicion))
                {
                    continue;
                }
       
                // Misma cuadricula
                if (unidadCuadriculaPosicion == testarCuadriculaPosicion)
                {
                    continue;
                }

                // Ya hay una unidad
                if(CuadriculaNivel.Instance.HayUnidadEnCuadriculaPosicion(testarCuadriculaPosicion))
                {
                    continue;
                }

                validoCuadriculoPosicionLista.Add(testarCuadriculaPosicion);

            }

        }

      

        return validoCuadriculoPosicionLista;
    }


}
