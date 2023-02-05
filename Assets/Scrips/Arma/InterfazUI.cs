using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfazUI : MonoBehaviour
{
    public int cantProyectiles;
    public int cantTotalProyectiles;

    public TextMeshProUGUI cantProyectilesTXT;
    public TextMeshProUGUI cantTotalProyectilesTXT;

    public void Disparar()
    {
        cantProyectiles--;
        cantProyectilesTXT.text = cantProyectiles + "/" + cantProyectiles;
    }

    public void Recargar(int cuanto)
    {
        cantProyectiles += cuanto;

        if (cantProyectiles < cantTotalProyectiles)
        {
            cantProyectiles = cantTotalProyectiles;
        }
        cantTotalProyectilesTXT.text = cantProyectiles + "/" + cantProyectiles;

    }
}
