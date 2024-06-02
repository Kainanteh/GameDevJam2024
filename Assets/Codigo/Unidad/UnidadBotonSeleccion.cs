using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadBotonSeleccion : MonoBehaviour
{

    [SerializeField] private Transform UnidadBoton;



    public void prepararInstanciarUnidad()
    {

        UnidadJugadorSeleccionada.Instance.UnidadSeleccionada = UnidadBoton.GetComponent<Unidad>();

        if(Nivel.Instance.Flecha1.gameObject.activeSelf && Nivel.Instance.Tutorial == true)
        {

            Nivel.Instance.Flecha1.gameObject.SetActive(false);
            Nivel.Instance.Flecha2.gameObject.SetActive(true);


        }

    }

}
