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
                        unidadObjetivo.Da�oAVida(50);
                    }
                }

            }
            else
            {

                // Si se ha seleccionado una unidad y la cuadricula es instanciadora (al lado del nucleo del jugador)
                if (unidad != null && cuadriculaObjeto.cuadriculaInstanciadora == true)
                {
                
      
                    if (
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 8)    ||
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 9 )   ||
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 10)   ||
                    (cuadriculaPosicion.x == 7 && cuadriculaPosicion.z == 11)
                    )
                    {
                        if (Nivel.Instance.Tutorial == true
                            || Nivel.Instance.Nivel1 == true
                            || Nivel.Instance.Nivel2 == true) { return; }

                        if (unidad == null || (NucleoPuntos.Instance.GetPuntosJugador() - unidad.costePuntosUnidad) < 0
             || cuadriculaObjeto.cuadriculaInstanciadora != true) { return; }
                        else if (unidad != null)
                        {

                            NucleoPuntos.Instance.SetPuntosJugador((unidad.costePuntosUnidad) * -1);
                        }

                        if (Nivel.Instance.Flecha2.gameObject.activeSelf && Nivel.Instance.Tutorial == true)
                        {

                            Nivel.Instance.Flecha2.gameObject.SetActive(false);

                        }


                        Unidad UnidadNueva = CuadriculaNivel.Instance.InstanciarUnidad(unidad.transform, cuadriculaPosicion);


                        UnidadNueva.SetDireccion(Unidad.Direccion.Oeste);
                        Nivel.Instance.UnidadesNucleoOeste.Add(UnidadNueva.transform);

                    }

                    if (
                    (cuadriculaPosicion.x == 8 && cuadriculaPosicion.z == 7) ||
                    (cuadriculaPosicion.x == 9 && cuadriculaPosicion.z == 7) ||
                    (cuadriculaPosicion.x == 10 && cuadriculaPosicion.z == 7) ||
                    (cuadriculaPosicion.x == 11 && cuadriculaPosicion.z == 7)
                    )
                    {

                        if (unidad == null || (NucleoPuntos.Instance.GetPuntosJugador() - unidad.costePuntosUnidad) < 0
             || cuadriculaObjeto.cuadriculaInstanciadora != true) { return; }
                        else if (unidad != null)
                        {

                            NucleoPuntos.Instance.SetPuntosJugador((unidad.costePuntosUnidad) * -1);
                        }

                        if (Nivel.Instance.Flecha2.gameObject.activeSelf && Nivel.Instance.Tutorial == true)
                        {

                            Nivel.Instance.Flecha2.gameObject.SetActive(false);

                        }


                        Unidad UnidadNueva = CuadriculaNivel.Instance.InstanciarUnidad(unidad.transform, cuadriculaPosicion);


                        UnidadNueva.SetDireccion(Unidad.Direccion.Sur);
                        Nivel.Instance.UnidadesNucleoSur.Add(UnidadNueva.transform);
    
                    }

                    if (
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 8) ||
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 9) ||
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 10) ||
                      (cuadriculaPosicion.x == 12 && cuadriculaPosicion.z == 11)
                      )
                    {


                        if (unidad == null || (NucleoPuntos.Instance.GetPuntosJugador() - unidad.costePuntosUnidad) < 0
             || cuadriculaObjeto.cuadriculaInstanciadora != true) { return; }
                        else if (unidad != null)
                        {

                            NucleoPuntos.Instance.SetPuntosJugador((unidad.costePuntosUnidad) * -1);
                        }

                        if (Nivel.Instance.Flecha2.gameObject.activeSelf && Nivel.Instance.Tutorial == true)
                        {

                            Nivel.Instance.Flecha2.gameObject.SetActive(false);

                        }


                        if (Nivel.Instance.Tutorial == true 
                            || Nivel.Instance.Nivel1 == true) { return; }

                        Unidad UnidadNueva = CuadriculaNivel.Instance.InstanciarUnidad(unidad.transform, cuadriculaPosicion);


                        UnidadNueva.SetDireccion(Unidad.Direccion.Este);
                        Nivel.Instance.UnidadesNucleoEste.Add(UnidadNueva.transform);

                    }

                    if (
                     (cuadriculaPosicion.x == 8 && cuadriculaPosicion.z == 12) ||
                     (cuadriculaPosicion.x == 9 && cuadriculaPosicion.z == 12) ||
                     (cuadriculaPosicion.x == 10 && cuadriculaPosicion.z == 12) ||
                     (cuadriculaPosicion.x == 11 && cuadriculaPosicion.z == 12)
                     )
                    {

                        if (unidad == null || (NucleoPuntos.Instance.GetPuntosJugador() - unidad.costePuntosUnidad) < 0
             || cuadriculaObjeto.cuadriculaInstanciadora != true) { return; }
                        else if (unidad != null)
                        {

                            NucleoPuntos.Instance.SetPuntosJugador((unidad.costePuntosUnidad) * -1);
                        }

                        if (Nivel.Instance.Flecha2.gameObject.activeSelf && Nivel.Instance.Tutorial == true)
                        {

                            Nivel.Instance.Flecha2.gameObject.SetActive(false);

                        }


                        if (Nivel.Instance.Tutorial == true
                         || Nivel.Instance.Nivel1 == true
                         || Nivel.Instance.Nivel2 == true
                         || Nivel.Instance.Nivel3 == true) { return; }

                        Unidad UnidadNueva = CuadriculaNivel.Instance.InstanciarUnidad(unidad.transform, cuadriculaPosicion);


                        UnidadNueva.SetDireccion(Unidad.Direccion.Norte);
                        Nivel.Instance.UnidadesNucleoNorte.Add(UnidadNueva.transform);

                    }

                    UnidadJugadorSeleccionada.Instance.LimpiarUnidadSeleccionada();



                }

            }

        }

    }

}
