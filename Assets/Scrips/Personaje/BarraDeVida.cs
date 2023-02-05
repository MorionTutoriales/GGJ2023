using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BarraDeVida : MonoBehaviour
{
    public Image barraDeVida;

    public float vidaActual;
    public float vidaMaxima;
    public UnityEvent eventoMorir;
    bool muerto = false;

    void Update()
    {
        barraDeVida.fillAmount = Mathf.Lerp(barraDeVida.fillAmount, vidaActual / vidaMaxima, 0.1f);
		if (vidaActual<=0 && !muerto)
		{
            eventoMorir.Invoke();
            muerto = true;
		}
    }
}
