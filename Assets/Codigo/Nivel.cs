using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel : MonoBehaviour
{

    public bool Tutorial;
    public bool Nivel1;
    public bool Nivel2;
    public bool Nivel3;
    public bool Nivel4;

    public Transform Flecha1;
    public Transform Flecha2;

    public Transform NucleoSur;
    public Transform NucleoEste;
    public Transform NucleoOeste;
    public Transform NucleoNorte;


    public List<Transform> UnidadesNucleoSur;
    public List<Transform> UnidadesNucleoNorte;
    public List<Transform> UnidadesNucleoEste;
    public List<Transform> UnidadesNucleoOeste;

    public Animator animatorNucleoSur;
    public Animator animatorNucleoNorte;
    public Animator animatorNucleoEste;
    public Animator animatorNucleoOeste;


    public static Nivel Instance
    {
        get; private set;
    }

 
    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("There's more than one Nivel!" + " - " + Instance);
            return;
        }

        Instance = this;

    }

    private void Start()
    {
        if (Tutorial == true)
        {
            NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
            animatorNucleoSur.SetTrigger("entrada");
        }
    }

    private void Update()
    {

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0) // Tutorial
        {
 

            if (Tutorial == true)
            {
                Debug.Log("Tutorial1ANivel1");
                Tutorial = false;
                Nivel1 = true;
                Tutorial1ANivel1();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().IANombreTXT = "NucleoSur";

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;

                animatorNucleoSur.SetTrigger("salida");
            }
        }

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0) // Nivel 1
        {
          

            if (Nivel1 == true)
            {
                Debug.Log("Nivel1ANivel2");
                Nivel1 = false;
                Nivel2 = true;
                Nivel1ANivel2();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;

                animatorNucleoSur.SetTrigger("salida");
                animatorNucleoEste.SetTrigger("entrada");
            }
        }

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0) // Nivel 2
        {
      

            if (Nivel2 == true)
            {
                Debug.Log("Nivel2ANivel3");
                Nivel2 = false;
                Nivel3 = true;
                Nivel2ANivel3();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoOeste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;
                NucleoOeste.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;

                animatorNucleoSur.SetTrigger("salida");
                animatorNucleoEste.SetTrigger("salida");
                animatorNucleoOeste.SetTrigger("entrada");
            }
        }

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0)  // Nivel 3
        {
      

            if (Nivel3 == true)
            {
                Debug.Log("Nivel3ANivel4");
                Nivel3 = false;
                Nivel4 = true;
                Nivel3ANivel4();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoNorte.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoOeste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoNorte.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;
                NucleoOeste.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;
                NucleoNorte.gameObject.GetComponent<UnidadInstanciaNucleo>().activado = true;


                animatorNucleoSur.SetTrigger("salida");
                animatorNucleoEste.SetTrigger("salida");
                animatorNucleoOeste.SetTrigger("salida");
                animatorNucleoNorte.SetTrigger("entrada");

            }
        }

        // Nivel 4
        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoNorte.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0)
        {

     

            if (Nivel4 == true)
            {

                Debug.Log("Ganar");

            }

        }

    }

    public void Tutorial1ANivel1()
    {

        //NucleoSur.gameObject.SetActive(false);
        NucleoSur.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();

        animatorNucleoSur.SetTrigger("salida");

        foreach (var unidad in UnidadesNucleoSur)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoSur.Clear();
        //NucleoSur.gameObject.SetActive(true);

 
    }

    public void Nivel1ANivel2()
    {


        NucleoSur.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoEste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();


        foreach (var unidad in UnidadesNucleoSur)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoSur.Clear();

        foreach (var unidad in UnidadesNucleoEste)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoEste.Clear();




    }

    public void Nivel2ANivel3()
    {


        NucleoSur.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoEste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoOeste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        


        foreach (var unidad in UnidadesNucleoSur)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoSur.Clear();

        foreach (var unidad in UnidadesNucleoEste)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoEste.Clear();

        foreach (var unidad in UnidadesNucleoOeste)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoOeste.Clear();



        NucleoPuntos.Instance.ratioPuntos = 2;

    }

    public void Nivel3ANivel4()
    {


        NucleoSur.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoEste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoOeste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();

        foreach (var unidad in UnidadesNucleoSur)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoSur.Clear();

        foreach (var unidad in UnidadesNucleoEste)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoEste.Clear();

        foreach (var unidad in UnidadesNucleoOeste)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoOeste.Clear();

        NucleoPuntos.Instance.ratioPuntos = 3;

 
    }


    public void Ganar()
    {

        NucleoSur.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoEste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoOeste.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();
        NucleoNorte.GetComponent<UnidadInstanciaNucleo>().StopEnemigosCoroutine();

        NucleoPuntos.Instance.PararPuntos = true;
        


    }


}
