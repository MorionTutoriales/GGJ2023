using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrquestadorInicial : MonoBehaviour
{
    public GameObject[] listadoInvertir;
    public float tiempoInvertir;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Invertir", tiempoInvertir);
    }

    // Update is called once per frame
    void Invertir()
    {
		for (int i = 0; i < listadoInvertir.Length; i++)
		{
            listadoInvertir[i].SetActive(!listadoInvertir[i].activeSelf);
		}
    }
}
