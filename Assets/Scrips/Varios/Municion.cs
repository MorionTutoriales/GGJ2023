using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Municion : MonoBehaviour
{
    public GameObject particulas;
    public static Municion objetoActivo;
    public Image imagenCarga;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnDestroy()
	{
        Instantiate(particulas, transform.position, Quaternion.identity);
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimiento2.singleton.enZonaMunicion = true;
            objetoActivo = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimiento2.singleton.enZonaMunicion = false;
			if (objetoActivo == this)
			{
                objetoActivo = null;
			}
        }
    }
}
