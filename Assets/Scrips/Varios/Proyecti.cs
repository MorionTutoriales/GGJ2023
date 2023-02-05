using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyecti : MonoBehaviour
{
    public float fuerzaInicial;
    Rigidbody rb;
    public GameObject[] objetos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward * fuerzaInicial);
        int o = Random.Range(0, objetos.Length);
		for (int i = 0; i < objetos.Length; i++)
		{
            objetos[i].SetActive(i == o);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
