using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAenemigo2 : MonoBehaviour
{
    public Estados estado;
    public Transform personaje;
    public float velocidad = 3f;
    public float vida = 100f;
    public Vector3 desfase;
    public float amplitud;
    private Vector3 pos;
    public float frecuencia;
    public GameObject particulas;

    private void Start()
    {
        pos = transform.position;
    }

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

        if ((personaje.position - transform.position).magnitude < 10)
        {
            CambiarEstado(Estados.Seguir);
        }
    }
    void EstadoSeguir()
    {
        Vector3 dir = ((personaje.position + desfase) - transform.position).normalized;
        pos = pos + dir * velocidad * Time.deltaTime;
        transform.position = pos + Vector3.up * Mathf.Sin(Time.time * frecuencia) * amplitud;
        //////// transiciones  ///////
        if (vida <= 0)
        {
            CambiarEstado(Estados.Muerto);
        }

        if ((personaje.position - transform.position).magnitude < 2)
        {
            CambiarEstado(Estados.Atacando);
        }
    }
    void EstadoAtacando()
    {
        print("Atacando");
        transform.position = pos + Vector3.up * Mathf.Sin(Time.time * frecuencia) * amplitud;

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

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (particulas != null)
        {
            GameObject effect = Instantiate(particulas);
            effect.transform.position = transform.position;
            
        }
    }
}
