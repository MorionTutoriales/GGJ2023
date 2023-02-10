using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFinal : MonoBehaviour
{
    public Transform jugador;
    public Vector2 tiemposAtacar;
    public float distanciaPeligrosa;
	public Animator animaciones;
	public Transform plataforma;
	public AlemiMorion alemic;
	public Transform punto;
	public GameObject proyectil;
	public static BossFinal singleton;
	public Instanciador[] instanciadores;

	public float vidaActual;
	public float vidaInicial = 100;
	bool activo;
	public BarraDeVida barraVida;
	public bool atacando;
	private void Awake()
	{
		singleton = this;
	}

	public void CargarMenu()
	{
		SceneManager.LoadScene("Niveles");
	}

	// Start is called before the first frame update
	IEnumerator Start()
    {
		vidaActual = vidaInicial;
        yield return new WaitForSeconds(8);
        jugador = Movimiento2.singleton.transform;
		plataforma.parent = null;
		while ((transform.position - jugador.position).magnitude > distanciaPeligrosa)
		{
			yield return new WaitForSeconds(0.5f);
		}
		alemic.enabled = true;
		yield return new WaitForSeconds(2);
		animaciones.SetBool("visible", true);
		transform.forward = (jugador.transform.position - transform.position).normalized;
		StartCoroutine(InstanciarEnemigos());
		activo = true;
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(tiemposAtacar.x, tiemposAtacar.y));
			
			if (atacando)
			{
				for (int i = 0; i < 40; i++)
				{
					yield return new WaitForSeconds(1 / 35f);
					transform.forward = Vector3.Lerp(transform.forward, (jugador.transform.position - transform.position).normalized, 0.5f);
				}
				yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
				animaciones.SetTrigger("atacar");
				yield return new WaitForSeconds(1);
				Instantiate(proyectil, punto.position, punto.rotation);
			}
		}
    }

	private void FixedUpdate()
	{
		atacando = (transform.position - jugador.position).magnitude < distanciaPeligrosa;
		FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Batalla", atacando?1:0);
	}

	public void QuitarVida(float cuanto)
	{
		vidaActual -= cuanto;
		barraVida.vidaActual = vidaActual;
		if (vidaActual<=0)
		{
			Morir();
		}
	}

	void Morir()
	{
		animaciones.SetBool("vivo", false);
		//Destroy(gameObject.GetComponent<Collider>());
		Destroy(this);
		Invoke("CargarMenu", 10);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, distanciaPeligrosa);
	}

	IEnumerator InstanciarEnemigos()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(10, 30));
			if (activo)
			{
				instanciadores[Random.Range(0, instanciadores.Length)].Instanciar();
			}
		}
	}
}
