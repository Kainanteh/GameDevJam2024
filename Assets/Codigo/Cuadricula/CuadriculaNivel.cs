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

    
    [SerializeField] private int Alto;
    [SerializeField] private int Ancho;

    [SerializeField] Transform parentDebug;

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

        cuadriculaSistema = new CuadriculaSistema(Alto, Ancho, 2f);

     /*   List<Transform> listaDebug = cuadriculaSistema.CrearDebugCuadriculaObjeto(CuadriculaDebugPrefab);

        foreach (Transform t in listaDebug)
        {
            t.parent = parentDebug;
            
        }*/

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

    public void UnidadSeHaMovidoCuadriculaPosicion(Unidad unidad, CuadriculaPosicion desdeCuadriculaPosicion, CuadriculaPosicion haciaCuadriculaPosicion)
    {

        LimpiarUnidadACuadriculaPosicion(desdeCuadriculaPosicion);
        SetUnidadACuadriculaPosicion(haciaCuadriculaPosicion, unidad);

    }

    public CuadriculaPosicion GetCuadriculaPosicion(Vector3 posicionMundo) 
    {
    
        return cuadriculaSistema.GetCuadriculaPosicion(posicionMundo);

    }

    public bool EsValidaCuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        return cuadriculaSistema.EsValidaCuadriculaPosicion(cuadriculaPosicion);

    }

    public Vector3 GetMundoPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        return new Vector3(cuadriculaPosicion.x, 0, cuadriculaPosicion.z) * cuadriculaSistema.GetTamañoCelda();

    }

    public int GetAlto()
    {
        return cuadriculaSistema.GetAlto();
    }
    public int GetAncho()
    {
        return cuadriculaSistema.GetAncho();
    }

    public CuadriculaSistema GetCuadriculaSistema()
    {
        return cuadriculaSistema;
    }

    public bool HayUnidadEnCuadriculaPosicion(CuadriculaPosicion cuadriculaPosicion)
    {

        CuadriculaObjeto cuadriculaObjeto = cuadriculaSistema.GetCuadriculaObjeto(cuadriculaPosicion);
        return cuadriculaObjeto.HayUnidad();

    }

    public Unidad InstanciarUnidad(Transform unidadAInstanciar, CuadriculaPosicion cuadriculaAInstanciar)
    {

       

     /*   System.Numerics.Vector3 systemVector = cuadriculaAInstanciar.GetVectorPosicion();

        UnityEngine.Vector3 unityVector = new UnityEngine.Vector3(systemVector.X * 2, systemVector.Y, systemVector.Z * 2);*/

        return Instantiate(unidadAInstanciar, GetMundoPosicion(cuadriculaAInstanciar), Quaternion.identity).
                GetComponent<Unidad>();
        



    }

}
