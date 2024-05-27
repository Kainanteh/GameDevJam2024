using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UnidadInstanciaNucleo : MonoBehaviour
{

    [SerializeField] private Transform Unidad;
    [SerializeField] private Transform UnidadEnemigo;

    [SerializeField] private List<CuadriculaPosicion> cuadriculasInstanciadoras;

    private Unidad estaUnidad;

    private void Awake()
    {
   
        estaUnidad = GetComponent<Unidad>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //InstanciarUnidad(Unidad);

        string path = Path.Combine(Application.dataPath, "IAEnemigo", "NucleoEste_1.txt");
        List<string> lineas = LeerArchivoTexto(path);
        ProcesarInstanciaciones(lineas);

    }


    public void InstanciarUnidad(Transform unidadAInstanciar)
    {

        foreach (var cuadriculasInst in cuadriculasInstanciadoras)
        {

            System.Numerics.Vector3 systemVector = cuadriculasInst.GetVectorPosicion();
           
            UnityEngine.Vector3 unityVector = new UnityEngine.Vector3(systemVector.X*2, systemVector.Y, systemVector.Z*2);

            Instantiate(unidadAInstanciar, unityVector, Quaternion.identity);
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

    public void ProcesarInstanciaciones(List<string> lineas)
    {
        foreach (var linea in lineas)
        {
            string[] partes = linea.Split(':');
            if (partes.Length == 3)
            {
                int segundos = int.Parse(partes[0]);
                int tipoEnemigo = int.Parse(partes[1]);
                int indiceCuadricula = int.Parse(partes[2]);

                StartCoroutine(InstanciarEnemigoConRetraso(segundos, tipoEnemigo, indiceCuadricula));
            }
        }
    }

    private IEnumerator InstanciarEnemigoConRetraso(int segundos, int tipoEnemigo, int indiceCuadricula)
    {
        yield return new WaitForSeconds(segundos);

        Transform enemigoAInstanciar = tipoEnemigo == 0 ? UnidadEnemigo : null; // Aquí puedes añadir más tipos de enemigos

        if (enemigoAInstanciar != null && indiceCuadricula >= 0 && indiceCuadricula < cuadriculasInstanciadoras.Count)
        {
            System.Numerics.Vector3 systemVector = cuadriculasInstanciadoras[indiceCuadricula].GetVectorPosicion();
            UnityEngine.Vector3 unityVector = new UnityEngine.Vector3(systemVector.X * 2, systemVector.Y, systemVector.Z * 2);
            Unidad unidadInstanciada = Instantiate(enemigoAInstanciar, unityVector, Quaternion.identity).GetComponent<Unidad>();
            unidadInstanciada.SetMoverseTrue();
            unidadInstanciada.SetDireccion(estaUnidad.GetDireccion());
        }
    }

}
