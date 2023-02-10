using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipuladorVida : MonoBehaviour
{
    BarraDeVida barraDeVida;

    public int cantidad;
    public float tiempoDa�o;
    float tiempoDeDa�oActual;
    void Start()
    {
        barraDeVida = GameObject.FindWithTag("Player").GetComponent<BarraDeVida>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            tiempoDeDa�oActual += Time.deltaTime;
            if(tiempoDeDa�oActual < tiempoDa�o)
            {
                barraDeVida.vidaActual -= cantidad;
                tiempoDeDa�oActual = 0.0f;
            }
        }
    }
}
