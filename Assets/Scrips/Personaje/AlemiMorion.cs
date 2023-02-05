using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.Events;

public class AlemiMorion : MonoBehaviour
{
    public float velocidad;
    float frame;
    AlembicStreamPlayer alem;
    public bool loop;
    public float loopTime;
    public UnityEvent eventoInicial;

    void Start()
    {
        alem = GetComponent<AlembicStreamPlayer>();
        eventoInicial.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        frame += Time.deltaTime*velocidad;
        alem.CurrentTime = frame;
		if (loop && frame > loopTime)
		{
            frame = 0;
		}
    }
}
