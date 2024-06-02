using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoManager : MonoBehaviour
{

    public List<AudioClip> AudiosLista;

    private AudioSource _AudioSource;

    public static SonidoManager Instance
    {
        get; private set;
    }



    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("There's more than one SonidoManager!" + " - " + Instance);
            return;
        }

        Instance = this;

        _AudioSource = GetComponent<AudioSource>();
   
    }

    public void ActivarSonido(int indice)
    {
        _AudioSource.PlayOneShot(AudiosLista[indice]);
    }

}
