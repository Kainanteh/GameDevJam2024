using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnosGolpeBajo : MonoBehaviour
{

    public static TurnosGolpeBajo Instance;

    [SerializeField]private bool esTurnoEnemigo;
    public bool EsTurnoEnemigo => esTurnoEnemigo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        esTurnoEnemigo = false; // Comienza con el turno del jugador
        AudioGolpeBajo.AudioGolpeBajoEvento += AlternarTurno;
    }

    private void OnDestroy()
    {
        AudioGolpeBajo.AudioGolpeBajoEvento -= AlternarTurno;
    }

    private void AlternarTurno(object sender, EventArgs e)
    {
        esTurnoEnemigo = !esTurnoEnemigo; // Alterna el turno
    }
}
