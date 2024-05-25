using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadriculaSistema 
{

    private int alto;
    private int ancho;
    private float tama�oCelda;

    private CuadriculaObjeto[,] cuadriculaObjetosArray;



    public CuadriculaSistema(int alto, int ancho, float tama�oCelda)
    {

        this.alto = alto;
        this.ancho = ancho;
        this.tama�oCelda = tama�oCelda;

        cuadriculaObjetosArray = new CuadriculaObjeto[alto, ancho];

        for (int x = 0; x < alto; x++)
        {

            for (int z = 0; z < ancho; z++)
            {

                CuadriculaPosicion cuadriculaPosicion = new CuadriculaPosicion(x,z);
                cuadriculaObjetosArray[x,z] = new CuadriculaObjeto(this, cuadriculaPosicion);

            }
        
        }

    }

    public Vector3 GetMundoPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        return new Vector3(cuadriculaPosicion.x, 0, cuadriculaPosicion.z) * tama�oCelda;

    }

    public CuadriculaPosicion GetCuadriculaPosicion(Vector3 posicionMundo)
    {
        return new CuadriculaPosicion(
            Mathf.RoundToInt( posicionMundo.x / tama�oCelda),
            Mathf.RoundToInt(posicionMundo.z / tama�oCelda));
    }

    public void CrearDebugCuadriculaObjeto(Transform debugPrefab)
    {

        for (int x = 0; x < alto; x++)
        {

            for (int z = 0; z < ancho; z++)
            {

                CuadriculaPosicion cuadriculaPosicion = new CuadriculaPosicion(x,z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetMundoPosicion(cuadriculaPosicion),Quaternion.identity);
                CuadriculaDebugObjeto cuadriculaDebugObjeto = debugTransform.GetComponent<CuadriculaDebugObjeto>();
                cuadriculaDebugObjeto.SetCuadriculaObjeto(GetCuadriculaObjeto(cuadriculaPosicion));


            }

        }


    }

    public CuadriculaObjeto GetCuadriculaObjeto(CuadriculaPosicion cuadriculaPosicion)
    {
        return cuadriculaObjetosArray[cuadriculaPosicion.x, cuadriculaPosicion.z];
    }

    public float GetTama�oCelda()
    {
        return tama�oCelda;
    }

}
