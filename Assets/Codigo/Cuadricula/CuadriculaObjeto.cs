using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadriculaObjeto 
{
 
    private CuadriculaSistema cuadriculaSistema;
    private CuadriculaPosicion cuadriculaPosicion;
    private Unidad unidad;

    public bool cuadriculaInstanciadora;

    public CuadriculaObjeto(CuadriculaSistema cuadriculaSistema, CuadriculaPosicion cuadriculaPosicion)
    { 
    
        this.cuadriculaSistema = cuadriculaSistema;
        this.cuadriculaPosicion = cuadriculaPosicion;

    }

    public override string ToString()
    {
        return cuadriculaPosicion.ToString() + "\n" + unidad;
    }

    public void SetUnidad(Unidad unidad)
    {
        this.unidad = unidad;
    }

    public Unidad GetUnidad() 
    {
    
        return this.unidad;
    
    }

    public bool HayUnidad()
    {
        return this.unidad != null;
    }

}
