using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Movimiento2 : MonoBehaviour
{
    public Transform target;
    public float velocidad;
    public Animator animaciones;

    public InputActionProperty prDisparo;
    public float frecuenciaDisparo;
    float ultimoDisparo;
    public float btnDisparar;
    public Transform posDisparo;
    public GameObject particulasDisparo;
    public GameObject bala;
    public InputActionProperty prSalto;
    public float ultimoSalto;
    public UnityEvent eventoSaltar;

    Vector3 posAnterior;
    public InputActionProperty prCarga;
    public float pCarga;
    public MonoBehaviour controlPrincipal;
    public bool enZonaMunicion;
    public static Movimiento2 singleton;
    public float velocidadCarga = 0.5f;
    public float cargaActual = 0;
    float tiempoUltimoTrigger;
    public bool yaSono = false;

    public int cuantoRecarga = 10;

	private void Awake()
	{
        singleton = this;
    }

	void Start()
    {
        prDisparo.action.Enable();
        prCarga.action.Enable();
        prSalto.action.Enable();
    }
	private void Update()
	{
        btnDisparar = prDisparo.action.ReadValue<float>();

		if (prSalto.action.ReadValue<float>() > 0 && Time.time > ultimoSalto)
		{
            ultimoSalto = Time.time + 1;
            eventoSaltar.Invoke();
        }

        if (InterfazUI.singleton.cantProyectiles > 0 && prDisparo.action.ReadValue<float>() > 0 && Time.time > ultimoDisparo && pCarga<0.1f)
		{
            ultimoDisparo = Time.time + frecuenciaDisparo;
            animaciones.SetTrigger("Shoot");
            InterfazUI.singleton.Disparar();
            Instantiate(bala, posDisparo.position, posDisparo.rotation);
            Invoke("InstanciarParticulas", 0.2f);
		}
		if (velocidad < 0.1f && enZonaMunicion)
		{
            pCarga = prCarga.action.ReadValue<float>();
            controlPrincipal.enabled = (!(pCarga > 0));
            if ((pCarga > 0)) {
                cargaActual += (velocidadCarga/5f) * Time.deltaTime;
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("cosechando", cargaActual);
				if (!yaSono)
				{
                    FMODUnity.RuntimeManager.PlayOneShot("event:/ArracachaMan/cargacosechando");
                    yaSono = true;
                }
            }
            if (pCarga < 0.1f) {
                cargaActual = 0;
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("cosechando", cargaActual);
                yaSono = false;
            }
			if (Municion.objetoActivo != null)
			{
                Municion.objetoActivo.imagenCarga.fillAmount = cargaActual;
			}
			if (cargaActual >= 1)
			{
				if (Time.time > tiempoUltimoTrigger)
				{
                    animaciones.SetTrigger("Carga2");
                    tiempoUltimoTrigger = Time.time + 5;
                }
                Invoke("DesCargar", 2);
            }

		}
    }

    public void InstanciarParticulas()
	{
        (Instantiate(particulasDisparo, posDisparo.position, posDisparo.rotation) as GameObject).transform.parent = posDisparo.transform;
    }

    public void DesCargar()
	{
        Destroy(Municion.objetoActivo.gameObject);
        enZonaMunicion = false;
        pCarga = 0;
        cargaActual = 0;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("cosechando", cargaActual);
        yaSono = false;
        controlPrincipal.enabled = true;
        InterfazUI.singleton.Recargar(cuantoRecarga);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        velocidad   = (transform.position - posAnterior).magnitude / Time.deltaTime;
        posAnterior = transform.position;
        animaciones.SetFloat("velocidad", velocidad);
    }

}
