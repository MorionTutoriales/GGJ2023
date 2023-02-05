using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipuladorVida : MonoBehaviour
{
    BarraDeVida barraDeVida;

    public int cantidad;
    public float tiempoDaño;
    float tiempoDeDañoActual;
    void Start()
    {
        barraDeVida = GameObject.FindWithTag("Player").GetComponent<BarraDeVida>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            tiempoDeDañoActual += Time.deltaTime;
            if(tiempoDeDañoActual < tiempoDaño)
            {
                barraDeVida.vidaActual -= cantidad;
                tiempoDeDañoActual = 0.0f;
            }
        }
    }
}
