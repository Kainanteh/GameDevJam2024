using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadVidaSistema : MonoBehaviour
{

    public event EventHandler EnMuerte;
    public event EventHandler EnDaño;

    [SerializeField] private int vida = 100;
    private int vidaMaxima;

    private void Awake()
    {
        vidaMaxima = vida;
    }


    public void Damage(int damageAmount)
    {

        vida -= damageAmount;

        if (vida < 0)
        {
            vida = 0;
        }

        EnDaño?.Invoke(this, EventArgs.Empty);

        if (vida == 0)
        {
            Die();
        }

        Debug.Log(vida);

    }

    private void Die()
    {
        EnMuerte?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)vida / vidaMaxima;
    }

}
