using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UnidadInstanciaNucleo : MonoBehaviour
{
    [SerializeField] private Transform Unidad;
    [SerializeField] private Transform UnidadEnemigoMurcielago;
    [SerializeField] private Transform UnidadEnemigoCerdo;
    [SerializeField] private Transform UnidadEnemigoPollo;

    [SerializeField] private List<CuadriculaPosicion> cuadriculasInstanciadoras;

    private Unidad estaUnidad;
    private bool procesandoInstanciaciones;

    private void Awake()
    {
        estaUnidad = GetComponent<Unidad>();
    }

    public string IANombreTXT;

    void Start()
    {
        if (estaUnidad.EsEnemigo())
        {
            string path = Path.Combine(Application.dataPath, "IAEnemigo", IANombreTXT+".txt");
            List<string> lineas = LeerArchivoTexto(path);
            procesandoInstanciaciones = true;
            StartCoroutine(ProcesarInstanciaciones(lineas));
        }
        else
        {
            foreach (var cuadricula in cuadriculasInstanciadoras)
            {
                CuadriculaObjeto cuadriculaObjeto = CuadriculaNivel.Instance.GetCuadriculaSistema().GetCuadriculaObjeto(cuadricula);
                cuadriculaObjeto.cuadriculaInstanciadora = true;
            }
        }
    }

    public List<string> LeerArchivoTexto(string path)
    {
        List<string> lineas = new List<string>();
        using (StreamReader sr = new StreamReader(path))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                lineas.Add(linea);
            }
        }
        return lineas;
    }

    public IEnumerator ProcesarInstanciaciones(List<string> lineas)
    {
        while (procesandoInstanciaciones)
        {
            foreach (var linea in lineas)
            {
                string[] partes = linea.Split(':');
                if (partes.Length == 3)
                {
                    int segundos = int.Parse(partes[0]);
                    int tipoEnemigo = int.Parse(partes[1]);
                    int indiceCuadricula = int.Parse(partes[2]);

                    yield return StartCoroutine(InstanciarEnemigoConRetraso(segundos, tipoEnemigo, indiceCuadricula));
                }
            }
        }
    }

    private IEnumerator InstanciarEnemigoConRetraso(int segundos, int tipoEnemigo, int indiceCuadricula)
    {
        yield return new WaitForSeconds(segundos);

        Transform enemigoAInstanciar =
        tipoEnemigo == 0 ? UnidadEnemigoMurcielago :
        tipoEnemigo == 1 ? UnidadEnemigoCerdo :
        tipoEnemigo == 2 ? UnidadEnemigoPollo :
        null;

        if (enemigoAInstanciar != null && indiceCuadricula >= 0 && indiceCuadricula < cuadriculasInstanciadoras.Count)
        {
            System.Numerics.Vector3 systemVector = cuadriculasInstanciadoras[indiceCuadricula].GetVectorPosicion();
            UnityEngine.Vector3 unityVector = new UnityEngine.Vector3(systemVector.X * 2, systemVector.Y, systemVector.Z * 2);
            if (!CuadriculaNivel.Instance.HayUnidadEnCuadriculaPosicion(CuadriculaNivel.Instance.GetCuadriculaPosicion(unityVector)))
            {
                Unidad unidadInstanciada = Instantiate(enemigoAInstanciar, unityVector, Quaternion.identity).GetComponent<Unidad>();
                unidadInstanciada.SetMoverseTrue();
                unidadInstanciada.SetDireccion(estaUnidad.GetDireccion());
            }
        }
    }

    // Método para detener el bucle si es necesario
    public void DetenerInstanciaciones()
    {
        procesandoInstanciaciones = false;
    }
}
