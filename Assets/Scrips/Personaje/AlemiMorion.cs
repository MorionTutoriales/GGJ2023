using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class AlemiMorion : MonoBehaviour
{
    public float velocidad;
    int frame;
    AlembicStreamPlayer alem;
    void Start()
    {
        alem = GetComponent<AlembicStreamPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        velocidad += Time.deltaTime*velocidad;
        alem.CurrentTime = velocidad;
    }
}
