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

    public List<CuadriculaPosicion> GetValidoAccionCuadriculaPosicionLista()
    {
        List<CuadriculaPosicion> validoCuadriculoPosicionLista = new List<CuadriculaPosicion>();

        return validoCuadriculoPosicionLista;
    }


}
