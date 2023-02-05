using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    IEnumerator Start()
    {
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
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(tiemposAtacar.x, tiemposAtacar.y));
			if ((transform.position-jugador.position).magnitude < distanciaPeligrosa)
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, distanciaPeligrosa);
	}


}
