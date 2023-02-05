using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    public GameObject objeto;
    public Transform posicion;
    public ParticleSystem particulas;

    void Start()
    {
        
    }
    [ContextMenu("Instanciar")]
    public void Instanciar()
	{
        Instantiate(objeto, posicion.position, posicion.rotation);
        particulas.Play(true);
	}
}
