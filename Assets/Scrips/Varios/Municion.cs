using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour
{
    public static GameObject objetoActivo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimiento2.singleton.enZonaMunicion = true;
            objetoActivo = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimiento2.singleton.enZonaMunicion = false;
			if (objetoActivo == this.gameObject)
			{
                objetoActivo = null;
			}
        }
    }
}
