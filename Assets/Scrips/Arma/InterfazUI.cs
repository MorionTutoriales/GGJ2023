using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfazUI : MonoBehaviour
{
    public int cantProyectiles;
    public int cantTotalProyectiles;
    public static InterfazUI singleton;

    public TextMeshProUGUI cantProyectilesTXT;

	private void Awake()
	{
        singleton = this;
        ActualizarUI();
	}

	public void Disparar()
    {
        cantProyectiles--;
        cantProyectilesTXT.text = cantProyectiles + "/" + cantTotalProyectiles;
    }

    public void ActualizarUI()
    {
        cantProyectilesTXT.text = cantProyectiles + "/" + cantTotalProyectiles;
    }

    public void Recargar(int cuanto)
    {
        cantProyectiles += cuanto;

        if (cantProyectiles < cantTotalProyectiles)
        {
            cantProyectiles = cantTotalProyectiles;
        }
        cantProyectilesTXT.text = cantProyectiles + "/" + cantTotalProyectiles;

    }
}
