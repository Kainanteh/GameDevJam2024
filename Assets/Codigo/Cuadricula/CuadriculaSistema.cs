using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadriculaSistema 
{

    private int alto;
    private int ancho;
    private float tamañoCelda;

    private CuadriculaObjeto[,] cuadriculaObjetosArray;



    public CuadriculaSistema(int alto, int ancho, float tamañoCelda)
    {

        this.alto = alto;
        this.ancho = ancho;
        this.tamañoCelda = tamañoCelda;

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

        return new Vector3(cuadriculaPosicion.x, 0, cuadriculaPosicion.z) * tamañoCelda;

    }

    public CuadriculaPosicion GetCuadriculaPosicion(Vector3 posicionMundo)
    {
        return new CuadriculaPosicion(
            Mathf.RoundToInt( posicionMundo.x / tamañoCelda),
            Mathf.RoundToInt(posicionMundo.z / tamañoCelda));
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

    public float GetTamañoCelda()
    {
        return tamañoCelda;
    }

}
