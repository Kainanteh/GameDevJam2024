using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadInstanciaJugador : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CuadriculaPosicion cuadriculaPosicion = CuadriculaNivel.Instance.GetCuadriculaPosicion(MouseWorld.GetPosition());
            CuadriculaObjeto cuadriculaObjeto = CuadriculaNivel.Instance.GetCuadriculaSistema().GetCuadriculaObjeto(cuadriculaPosicion);

            Unidad unidad = UnidadJugadorSeleccionada.Instance.UnidadSeleccionada;

            // Si se ha seleccionado una unidad y la cuadricula es instanciadora (al lado del nucleo del jugador)
            if (unidad != null && cuadriculaObjeto.cuadriculaInstanciadora == true)
            {
                CuadriculaNivel.Instance.InstanciarUnidad(unidad.transform,cuadriculaPosicion);
                UnidadJugadorSeleccionada.Instance.LimpiarUnidadSeleccionada();

                Debug.Log(cuadriculaPosicion);
                if (
                cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 8  ||
                cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 9  ||
                cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 10 ||
                cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 11
                )
                {
                    unidad.SetDireccion(Unidad.Direccion.Oeste);
                } 
                


                
            }

        }
    }

}
