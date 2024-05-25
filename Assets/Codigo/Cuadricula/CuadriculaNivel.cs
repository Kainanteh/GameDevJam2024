using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadriculaNivel : MonoBehaviour
{
    public static CuadriculaNivel Instance
    {
        get; private set;
    }

    [SerializeField] private CuadriculaSistema cuadriculaSistema;
    [SerializeField] private Transform CuadriculaDebugPrefab;

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("There's more than one CuadriculaNivel!" + " - " + Instance);
            return;
        }

        Instance = this;



    }

    private void Start()
    {
        cuadriculaSistema = new CuadriculaSistema(10, 10, 2f);

        Debug.Log(new CuadriculaPosicion(5, 7));
        cuadriculaSistema.CrearDebugCuadriculaObjeto(CuadriculaDebugPrefab);
    }

    public void SetUnidadACuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion, Unidad unidad)
    {
        CuadriculaObjeto cuadriculaObjeto = cuadriculaSistema.GetCuadriculaObjeto(cuadriculaPosicion);
        cuadriculaObjeto.SetUnidad(unidad);
    }

    public Unidad GetUnidadACuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {
        CuadriculaObjeto cuadriculaObjeto = cuadriculaSistema.GetCuadriculaObjeto(cuadriculaPosicion);
        return cuadriculaObjeto.GetUnidad();
    }

    public void LimpiarUnidadACuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {
        CuadriculaObjeto cuadriculaObjeto = cuadriculaSistema.GetCuadriculaObjeto(cuadriculaPosicion);
        cuadriculaObjeto.SetUnidad(null);
    }

    public void UnidadMoverCuadriculaPosicion(Unidad unidad, CuadriculaPosicion desdeCuadriculaPosicion, CuadriculaPosicion haciaCuadriculaPosicion)
    {

        LimpiarUnidadACuadriculaPosicion(desdeCuadriculaPosicion);
        SetUnidadACuadriculaPosicion(haciaCuadriculaPosicion, unidad);

    }

    public CuadriculaPosicion GetCuadriculaPosicion(Vector3 posicionMundo) 
    {
    
        return cuadriculaSistema.GetCuadriculaPosicion(posicionMundo);

    }

    public Vector3 GetMundoPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        return new Vector3(cuadriculaPosicion.x, 0, cuadriculaPosicion.z) * cuadriculaSistema.GetTamañoCelda();

    }

}
