using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuadriculaDebugObjeto : MonoBehaviour
{

    private CuadriculaObjeto cuadriculaObjeto;

    [SerializeField] private TextMeshPro textMeshPro;

    public void SetCuadriculaObjeto(CuadriculaObjeto cuadriculaObjeto)
    {
        this.cuadriculaObjeto = cuadriculaObjeto;
    }

    private void Update()
    {
        textMeshPro.text = cuadriculaObjeto.ToString();
    }

}
