using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    FMOD.Studio.EventInstance Open, Close;
    [SerializeField] [FMODUnity.EventRef] private string sourceOpen, sourceClose;


    void Start()
    {
        Open = FMODUnity.RuntimeManager.CreateInstance(sourceOpen);
        Close = FMODUnity.RuntimeManager.CreateInstance(sourceClose);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Close, transform);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Open, transform);
    }

    public void OpenSound()
    {
        Open.start();
    }
    public void CloseSound()
    {
        Close.start();
    }
}
