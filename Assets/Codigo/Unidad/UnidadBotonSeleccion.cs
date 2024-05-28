using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadBotonSeleccion : MonoBehaviour
{

    [SerializeField] private Transform UnidadBoton;



    public void prepararInstanciarUnidad()
    {

        UnidadJugadorSeleccionada.Instance.UnidadSeleccionada = UnidadBoton.GetComponent<Unidad>();

    }

}
