using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
 
    [SerializeField] private CuadriculaSistema cuadriculaSistema;
    [SerializeField] private Transform CuadriculaDebugPrefab;

    private void Start()
    {
        cuadriculaSistema = new CuadriculaSistema(10,10,2f);

        Debug.Log(new CuadriculaPosicion(5,7));
        cuadriculaSistema.CrearDebugCuadriculaObjeto(CuadriculaDebugPrefab);
    }

    private void Update()
    {

        /*Debug.Log(cuadriculaSistema.GetCuadriculaPosicion(MouseWorld.GetPosition()));*/

      

    }

}
