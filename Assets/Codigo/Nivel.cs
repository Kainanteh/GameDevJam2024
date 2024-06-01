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


}
