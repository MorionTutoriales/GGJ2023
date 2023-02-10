using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public float velocidad = 3f;
    public GameObject particulas;
    public float daño=10;
    void Start()
    {
        //transform.LookAt(Movimiento2.singleton.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
        Instantiate(particulas, transform.position, transform.rotation);
		if (other.CompareTag("Player"))
		{
            Movimiento2.singleton.CausarDaño(daño);
            Destroy(gameObject);
		}
	}
}
