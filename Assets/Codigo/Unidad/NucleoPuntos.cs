using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NucleoPuntos : MonoBehaviour
{

    public int PuntosJugador = 0;

    public TextMeshPro textMeshPro;

    private void Start()
    {
        AudioGolpeBajo.AudioGolpeBajoEvento += PuntosJugadorGolpeBajo;
        SetTextPuntos();
    }

    public void SetTextPuntos()
    {
        textMeshPro.text = PuntosJugador.ToString();
    }

    public int GetPuntosJugador()
    {
        return PuntosJugador;
    }

    public void SetPuntosJugador(int puntos)
    {
        if ((PuntosJugador + puntos) >= 999)
        {
            PuntosJugador = 999;
        }
        else
        {
            PuntosJugador += puntos;
        }

        SetTextPuntos();
    }

    public void PuntosJugadorGolpeBajo(object sender, EventArgs e)
    {
        SetPuntosJugador(2);
    }


}
