using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento2 : MonoBehaviour
{
    public Transform target;
    public float velocidad;
    public Animator animaciones;

    public InputActionProperty prDisparo;
    public float frecuenciaDisparo;
    float ultimoDisparo;
    public float btnDisparar;

    Vector3 posAnterior;
    public InputActionProperty prCarga;
    public float pCarga;
    public MonoBehaviour controlPrincipal;
    public bool enZonaMunicion;
    public static Movimiento2 singleton;
    public float velocidadCarga = 0.5f;
    public float cargaActual = 0;

    void Start()
    {
        singleton = this;
        prDisparo.action.Enable();
        prCarga.action.Enable();
    }
	private void Update()
	{
        btnDisparar = prDisparo.action.ReadValue<float>();

        if (prDisparo.action.ReadValue<float>()>0 && Time.time > ultimoDisparo && pCarga<0.1f)
		{
            ultimoDisparo = Time.time + frecuenciaDisparo;
            animaciones.SetTrigger("Shoot");
		}
		if (velocidad < 0.1f && enZonaMunicion)
		{
            pCarga = prCarga.action.ReadValue<float>();
            controlPrincipal.enabled = (!(pCarga > 0));
            if ((pCarga > 0)) cargaActual += (velocidadCarga/5f) * Time.deltaTime;
			if (cargaActual >= 1)
			{
                animaciones.SetTrigger("Carga2");
                DesCargar();

            }

		}
    }

    public void DesCargar()
	{
        Destroy(Municion.objetoActivo);
        enZonaMunicion = false;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        velocidad   = (transform.position - posAnterior).magnitude / Time.deltaTime;
        posAnterior = transform.position;
        animaciones.SetFloat("velocidad", velocidad);
    }

}
