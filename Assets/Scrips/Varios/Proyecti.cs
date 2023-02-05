using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proyecti : MonoBehaviour
{
    public float fuerzaInicial;
    Rigidbody rb;
    public GameObject[] objetos;
    public GameObject particulasExplo;
    public float daño = 10;
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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Gusano"))
		{
            Instantiate(particulasExplo, transform.position, transform.rotation);
            BossFinal.singleton.QuitarVida(daño);
            Destroy(gameObject);
		}
	}
}
