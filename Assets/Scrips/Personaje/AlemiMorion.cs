using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class AlemiMorion : MonoBehaviour
{
    public float velocidad;
    float frame;
    AlembicStreamPlayer alem;
    public bool loop;
    public float loopTime;

    void Start()
    {
        alem = GetComponent<AlembicStreamPlayer>();
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
