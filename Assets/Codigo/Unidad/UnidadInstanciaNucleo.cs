using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class UnidadInstanciaNucleo : MonoBehaviour
{
    [SerializeField] private Transform Unidad;
    [SerializeField] private Transform UnidadEnemigoMurcielago;
    [SerializeField] private Transform UnidadEnemigoCerdo;
    [SerializeField] private Transform UnidadEnemigoPollo;

    [SerializeField] private List<CuadriculaPosicion> cuadriculasInstanciadoras;

    private Unidad estaUnidad;



    [SerializeField]private Coroutine EnemigosCoroutine;
    public bool activado = true;

    private void Awake()
    {
        estaUnidad = GetComponent<Unidad>();
    }

    public string IANombreTXT;

    void Start()
    {
        if (estaUnidad.EsEnemigo())
        {
            /*        if (Nivel.Instance.Tutorial == true)
                    {
                        string path = Path.Combine(Application.dataPath, "IAEnemigo", IANombreTXT + ".txt");
                        List<string> lineas = LeerArchivoTexto(path);
                        procesandoInstanciaciones = true;
                        StartCoroutine(ProcesarInstanciaciones(lineas));
                    }*/
         /*   if (Nivel.Instance.Tutorial == true)
            {
                EnemigosEmpezar();
            }*/
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

    /*   private void OnEnable()
       {
           if (Nivel.Instance.Tutorial == false)
           {
               if (estaUnidad.EsEnemigo())
               {
                   string path = Path.Combine(Application.dataPath, "IAEnemigo", IANombreTXT + ".txt");
                   List<string> lineas = LeerArchivoTexto(path);
                   procesandoInstanciaciones = true;
                   StartCoroutine(ProcesarInstanciaciones(lineas));
               }
           }
       }*/

    private void Update()
    {
        if (estaUnidad != null && estaUnidad.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && activado == true && EnemigosCoroutine != null)
        {
            //StopCoroutine(EnemigosCoroutine);
            activado = false;
        }

    }

    public void EnemigosEmpezar()
    {

        string path = Path.Combine(Application.streamingAssetsPath, "IAEnemigo", IANombreTXT + ".txt");
        List<string> lineas = LeerArchivoTexto(path);
   
        /*if (EnemigosCoroutine != null)*/

        if(estaUnidad != null && estaUnidad.GetComponent<UnidadVidaSistema>().GetVida() <= 0)
        {
            //StopCoroutine(EnemigosCoroutine);

        }

        // Iniciar una nueva Coroutine y almacenar la referencia
        EnemigosCoroutine = StartCoroutine(ProcesarInstanciaciones(lineas));
        
     
    }

    public void StopEnemigosCoroutine()
    {

        if (EnemigosCoroutine != null)
        {
         
            StopCoroutine(EnemigosCoroutine);
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
               
                switch(estaUnidad.GetDireccion())
                {
                    case global::Unidad.Direccion.Norte:
                    {

                        Nivel.Instance.UnidadesNucleoSur.Add(unidadInstanciada.transform);

                        break;
                    }
                    case global::Unidad.Direccion.Sur:
                    {

                        Nivel.Instance.UnidadesNucleoNorte.Add(unidadInstanciada.transform);

                        break;
                    }
                    case global::Unidad.Direccion.Este:
                    {

                        Nivel.Instance.UnidadesNucleoOeste.Add(unidadInstanciada.transform);

                        break;
                    }
                    case global::Unidad.Direccion.Oeste:
                    {

                        Nivel.Instance.UnidadesNucleoEste.Add(unidadInstanciada.transform);

                        break;
                    }

                }

            }

        }

        if (Nivel.Instance.Tutorial == true)
        {
            StopCoroutine(EnemigosCoroutine);
        }

    }

 
   
}
