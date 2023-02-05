using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Municion : MonoBehaviour
{
    public GameObject particulas;
    public static Municion objetoActivo;
    public Image imagenCarga;
    public Animator anim;

    // Start is called before the first frame update
    void Update()
    {
		if (objetoActivo != null && objetoActivo==this)
		{
            transform.forward = (Camera.main.transform.position - transform.position).normalized;
        }
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
            anim.SetBool("visible", true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("visible", false);
            Movimiento2.singleton.enZonaMunicion = false;
			if (objetoActivo == this)
			{
                objetoActivo = null;
			}
        }
    }
}
