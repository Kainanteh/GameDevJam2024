using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadJugadorSeleccionada : MonoBehaviour
{

    public Unidad UnidadSeleccionada;

    public static UnidadJugadorSeleccionada Instance
    {
        get; private set;
    }

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("There's more than one UnidadJugadorSeleccionada!" + " - " + Instance);
            return;
        }

        Instance = this;

    }

    public void LimpiarUnidadSeleccionada()
    {
        UnidadSeleccionada = null;
    }

}
