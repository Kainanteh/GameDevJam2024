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

            if (CuadriculaNivel.Instance.EsValidaCuadriculaPosicion(cuadriculaPosicion) == false) { return; }

            

            CuadriculaObjeto cuadriculaObjeto = CuadriculaNivel.Instance.GetCuadriculaSistema().GetCuadriculaObjeto(cuadriculaPosicion);

            Unidad unidad = UnidadJugadorSeleccionada.Instance.UnidadSeleccionada;


            if (CuadriculaNivel.Instance.HayUnidadEnCuadriculaPosicion(cuadriculaPosicion))
            {

                if (CuadriculaNivel.Instance.GetUnidadACuadriculaPosicion(cuadriculaPosicion).EsEnemigo())
                {
                    Unidad unidadObjetivo = CuadriculaNivel.Instance.GetUnidadACuadriculaPosicion(cuadriculaPosicion);

                    if (unidadObjetivo.NucleoEnemigo == false)
                    {
                        unidadObjetivo.DañoAVida(50);
                    }
                }

            }
            else
            {

                // Si se ha seleccionado una unidad y la cuadricula es instanciadora (al lado del nucleo del jugador)
                if (unidad != null && cuadriculaObjeto.cuadriculaInstanciadora == true)
                {
                    Unidad UnidadNueva = CuadriculaNivel.Instance.InstanciarUnidad(unidad.transform, cuadriculaPosicion);

      
                    if (
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 8)    ||
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 9 )   ||
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 10)   ||
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 11)
                    )
                    {
                        UnidadNueva.SetDireccion(Unidad.Direccion.Oeste);
            
                    }

                    if (
                    (cuadriculaPosicion.x == 8 && cuadriculaPosicion.z == 7) ||
                    (cuadriculaPosicion.x == 9 && cuadriculaPosicion.z == 7) ||
                    (cuadriculaPosicion.x == 10 && cuadriculaPosicion.z == 7) ||
                    (cuadriculaPosicion.x == 11 && cuadriculaPosicion.z == 7)
                    )
                    {
                        UnidadNueva.SetDireccion(Unidad.Direccion.Sur);
    
                    }

                    if (
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 8) ||
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 9) ||
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 10) ||
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 11)
                      )
                    {
                        UnidadNueva.SetDireccion(Unidad.Direccion.Este);

                    }

                    if (
                     (cuadriculaPosicion.x == 8 && cuadriculaPosicion.z == 12) ||
                     (cuadriculaPosicion.x == 9 && cuadriculaPosicion.z == 12) ||
                     (cuadriculaPosicion.x == 10 && cuadriculaPosicion.z == 12) ||
                     (cuadriculaPosicion.x == 11 && cuadriculaPosicion.z == 12)
                     )
                    {
                        UnidadNueva.SetDireccion(Unidad.Direccion.Norte);

                    }

                    UnidadJugadorSeleccionada.Instance.LimpiarUnidadSeleccionada();



                }

            }

        }

    }

}
