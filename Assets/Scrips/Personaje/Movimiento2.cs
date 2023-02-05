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

    void Start()
    {
        prDisparo.action.Enable();
        prCarga.action.Enable();
    }
	private void Update()
	{
        btnDisparar = prDisparo.action.ReadValue<float>();

        if (prDisparo.action.ReadValue<float>()>0 && Time.time > ultimoDisparo)
		{
            ultimoDisparo = Time.time + frecuenciaDisparo;
            animaciones.SetTrigger("Shoot");
		}
        pCarga = prCarga.action.ReadValue<float>();
        controlPrincipal.enabled = (!(pCarga > 0));
        animaciones.SetFloat("Carga", pCarga);
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        velocidad   = (transform.position - posAnterior).magnitude / Time.deltaTime;
        posAnterior = transform.position;
        animaciones.SetFloat("velocidad", velocidad);
    }
}
