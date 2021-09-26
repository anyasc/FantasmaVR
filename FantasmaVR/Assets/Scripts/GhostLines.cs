using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLines : MonoBehaviour
{

    FMOD.Studio.EventInstance Fala;
    [SerializeField] [FMODUnity.EventRef] private string source;
    [SerializeField] private Transform soundEmitter;

    // Start is called before the first frame update
    void Start()
    {
        if (soundEmitter == null)
        {
            soundEmitter = transform;
        }
    }


    public void Play()
    {
        Fala = FMODUnity.RuntimeManager.CreateInstance(source);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Fala, soundEmitter);
        Fala.start();
    }
}
