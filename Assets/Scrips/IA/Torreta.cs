using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
public Transform target;
public Transform turret ;
public Transform bullet;
public Transform bulletSpawn;
private float timer = 0.0f;

    private void Start()
    {
        
    }
    void OnTriggerStay(Collider objectTriggered)
    {
        timer += Time.deltaTime;
        if (objectTriggered.transform == target)
        {
            turret.transform.LookAt(target);

            if (timer > 0.1)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                timer = 0.0f;

            }

        }
    }
}
