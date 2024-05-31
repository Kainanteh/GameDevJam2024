using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadAnimacionDestruir : MonoBehaviour
{

    public GameObject padre;

    public void Destruir()
    {
        Destroy(padre);
    }

}
