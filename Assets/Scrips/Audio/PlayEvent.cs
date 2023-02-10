using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEvent : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEvento(string evento)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(evento);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        instance.start();
    }
}
