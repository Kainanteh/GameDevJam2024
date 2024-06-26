using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadriculaSistema 
{

    private int alto;
    private int ancho;
    private float tamaņoCelda;

    private CuadriculaObjeto[,] cuadriculaObjetosArray;



    public CuadriculaSistema(int alto, int ancho, float tamaņoCelda)
    {

        this.alto = alto;
        this.ancho = ancho;
        this.tamaņoCelda = tamaņoCelda;

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

        return new Vector3(cuadriculaPosicion.x, 0, cuadriculaPosicion.z) * tamaņoCelda;

    }

    public CuadriculaPosicion GetCuadriculaPosicion(Vector3 posicionMundo)
    {
        return new CuadriculaPosicion(
            Mathf.RoundToInt( posicionMundo.x / tamaņoCelda),
            Mathf.RoundToInt(posicionMundo.z / tamaņoCelda));
    }

    public List<Transform> CrearDebugCuadriculaObjeto(Transform debugPrefab)
    {

        List<Transform> debugLista = new List<Transform>();

        for (int x = 0; x < alto; x++)
        {

            for (int z = 0; z < ancho; z++)
            {

                CuadriculaPosicion cuadriculaPosicion = new CuadriculaPosicion(x,z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetMundoPosicion(cuadriculaPosicion),Quaternion.identity);
                CuadriculaDebugObjeto cuadriculaDebugObjeto = debugTransform.GetComponent<CuadriculaDebugObjeto>();
                cuadriculaDebugObjeto.SetCuadriculaObjeto(GetCuadriculaObjeto(cuadriculaPosicion));
                debugTransform.name = "CuadriculaDebug" + alto + " " + ancho;
                debugLista.Add(debugTransform);

            }

        }

        return debugLista;
    }

    public CuadriculaObjeto GetCuadriculaObjeto(CuadriculaPosicion cuadriculaPosicion)
    {
        return cuadriculaObjetosArray[cuadriculaPosicion.x, cuadriculaPosicion.z];
    }

    public float GetTamaņoCelda()
    {
        return tamaņoCelda;
    }
    public int GetAlto()
    {
        return alto;
    }
    public int GetAncho()
    {
        return ancho;
    }

    public bool EsValidaCuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {
      

        return cuadriculaPosicion.x >= 0 &&
                cuadriculaPosicion.z >= 0 &&
                cuadriculaPosicion.x < alto &&
                cuadriculaPosicion.z < ancho;
    }

}
