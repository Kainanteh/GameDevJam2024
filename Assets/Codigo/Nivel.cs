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

    private void Update()
    {

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0) // Tutorial
        {
            Debug.Log("Tutorial1ANivel1");

            if (Tutorial == true)
            {
                Tutorial = false;
                Nivel1 = true;
                Tutorial1ANivel1();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().IANombreTXT = "NucleoSur";

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
            }
        }

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0) // Nivel 1
        {
            Debug.Log("Nivel1ANivel2");

            if (Nivel1 == true)
            {
                Nivel1 = false;
                Nivel2 = true;
                Nivel1ANivel2();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
            }
        }

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0) // Nivel 2
        {
            Debug.Log("Nivel2ANivel3");

            if (Nivel2 == true)
            {
                Nivel2 = false;
                Nivel3 = true;
                Nivel2ANivel3();
                NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();
                NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().SetVidaMaximaVida();

                NucleoSur.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoEste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
                NucleoOeste.gameObject.GetComponent<UnidadInstanciaNucleo>().EnemigosEmpezar();
            }
        }

        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0)  // Nivel 3
        {
            Debug.Log("Nivel3ANivel4");

            if (Nivel3 == true)
            {
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
            }
        }

        // Nivel 4
        if (NucleoSur.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoEste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoOeste.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0 && NucleoNorte.gameObject.GetComponent<UnidadVidaSistema>().GetVida() <= 0)
        {

            Debug.Log("Ganar");

            if (Nivel4 == true)
            {



            }

        }

    }

    public void Tutorial1ANivel1()
    {

        NucleoSur.gameObject.SetActive(false);
 
        foreach(var unidad in UnidadesNucleoSur)
        {
            if (unidad != null)
            {
                Destroy(unidad.gameObject);
            }

        }
        UnidadesNucleoSur.Clear();
        NucleoSur.gameObject.SetActive(true);
 
    }

    public void Nivel1ANivel2()
    {

        NucleoSur.gameObject.SetActive(false);
        NucleoEste.gameObject.SetActive(false);


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


        NucleoSur.gameObject.SetActive(true);
        NucleoEste.gameObject.SetActive(true);

    }

    public void Nivel2ANivel3()
    {

        NucleoSur.gameObject.SetActive(false);
        NucleoEste.gameObject.SetActive(false);
        NucleoOeste.gameObject.SetActive(false);


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

        NucleoSur.gameObject.SetActive(true);
        NucleoEste.gameObject.SetActive(true);
        NucleoOeste.gameObject.SetActive(true);

        NucleoPuntos.Instance.ratioPuntos = 2;

    }

    public void Nivel3ANivel4()
    {

        NucleoSur.gameObject.SetActive(false);
        NucleoEste.gameObject.SetActive(false);
        NucleoOeste.gameObject.SetActive(false);


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

        NucleoSur.gameObject.SetActive(true);
        NucleoEste.gameObject.SetActive(true);
        NucleoOeste.gameObject.SetActive(true);
        NucleoNorte.gameObject.SetActive(true);

    }


}
