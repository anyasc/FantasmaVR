using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitrola : MonoBehaviour
{
    public bool focus;
    FMOD.Studio.EventInstance Jazz;
    [SerializeField] [FMODUnity.EventRef] private string source;
    public Animator animator;


    private void Start()
    {

        Jazz = FMODUnity.RuntimeManager.CreateInstance(source);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Jazz, transform);

        animator.SetBool("Aberta", true);
    }


    private void Update()
    {
        if (IsPlaying(Jazz))
        {
            if (focus && Input.GetButtonDown("Z"))
            {
                StopJazz();
            }
        }
        else
        {
            if (focus && Input.GetButtonDown("Z"))
            {
                StartJazz();
            }
        }
    }
    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
    }

    private void StartJazz()
    {
        animator.SetBool("Aberta", false);
        Jazz.start();
    }

    private void StopJazz()
    {
        animator.SetBool("Aberta", true);
        Jazz.stop(STOP_MODE.IMMEDIATE);
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
