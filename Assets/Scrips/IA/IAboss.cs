using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAboss : MonoBehaviour
{
    public Estados estado;
    public Transform personaje;
    public float velocidad = 3f;
    public float vida = 100f;

    void Update()
    {
        switch (estado)
        {
            case Estados.Idle:
                EstadoIdle();
                break;
            case Estados.Seguir:
                EstadoSeguir();
                break;
            case Estados.Atacando:
                EstadoAtacando();
                break;
            case Estados.Muerto:
                EstadoMuerte();
                break;
            default:
                break;
        }
    }

    void EstadoIdle()
    {

        //////// transiciones  ///////
        if (vida <= 0)
        {
            CambiarEstado(Estados.Muerto);
        }

        if ((personaje.position - transform.position).magnitude < 5)
        {
            CambiarEstado(Estados.Atacando);
        }
    }
    void EstadoSeguir()
    {
        
    }
    void EstadoAtacando()
    {
        print("AtacandoBoss");

        //////// transiciones  ///////
        if ((personaje.position - transform.position).magnitude > 3)
        {
            CambiarEstado(Estados.Idle);
        }

        if (vida <= 0)
        {
            CambiarEstado(Estados.Muerto);
        }

    }
    void EstadoMuerte()
    {

    }

    public void CambiarEstado(Estados nuevoEstado)
    {
        estado = nuevoEstado;
    }
}
