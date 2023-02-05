using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour
{
    public Transform jugador;

    // Start is called before the first frame update
    void Start()
    {
        jugador = Movimiento2.singleton.transform;
    }


}
