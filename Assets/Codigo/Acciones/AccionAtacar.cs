using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionAtacar : MonoBehaviour
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

 

    }

    public void Atacar(Unidad unidadObjetivo)
    {
        Debug.Log("Ataque a " + unidadObjetivo );
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


}
