using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAenemigo1 : MonoBehaviour
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

        if ((personaje.position-transform.position).magnitude < 5)
        {
            CambiarEstado(Estados.Seguir);
        }
    }
    void EstadoSeguir()
    {
        transform.LookAt(personaje);
        transform.Translate(0, 0, velocidad * Time.deltaTime);

        //////// transiciones  ///////
        if (vida <= 0)
        {
            CambiarEstado(Estados.Muerto);
        }

        if ((personaje.position-transform.position).magnitude < 2)
        {
            CambiarEstado(Estados.Atacando);
        }
    }
    void EstadoAtacando()
    {
        print("Atacando");

        //////// transiciones  ///////
        if ((personaje.position - transform.position).magnitude > 3)
        {
            CambiarEstado(Estados.Seguir);
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

public enum Estados
{
    Idle = 0, 
    Seguir = 1,
    Atacando = 2,
    Muerto = 3
}