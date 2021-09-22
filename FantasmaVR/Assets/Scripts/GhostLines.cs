using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLines : MonoBehaviour
{

    FMOD.Studio.EventInstance VoceNaoEhSusana;

    // Start is called before the first frame update
    void Start()
    {
        VoceNaoEhSusana = FMODUnity.RuntimeManager.CreateInstance("event:/VoceNaoEhSusana");
    }

    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(VoceNaoEhSusana, transform);
    }


    public void PlayVoceNaoEhSusana()
    {
        VoceNaoEhSusana.start();
    }
}
