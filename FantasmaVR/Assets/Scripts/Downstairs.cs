using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downstairs : MonoBehaviour
{
    FMOD.Studio.EventInstance Crowbar;
    public Transform door, basementPosition, upstairsPosition;
    public bool covered = true;
    public bool opened, focus;
    public bool inBasement = false;


    public Player player;

    void Start()
    {
        Crowbar = FMODUnity.RuntimeManager.CreateInstance("event:/Crowbar");
    }

    void Update()
    {
        if (focus && !covered && Input.GetButtonDown("Z"))
        {
            if (opened)
            {
                Teleport();
            }
            else if (!player.menuOpened)
            {
                player.Message("Está emperrado... Não consigo abrir");
            }
        }

    }

    public void Open()
    {
        StartCoroutine(OpenAnimationSound());
    }


    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
    }
    public void Teleport()
    {
        if (!inBasement)
        {
            player.transform.position = basementPosition.position;
            // Ir para porão
        }
        else
        {
            player.transform.position = upstairsPosition.position;
            // Voltar para térreo
        }
        inBasement = !inBasement;
    }

    IEnumerator OpenAnimationSound()
    {
        Crowbar.start();
        yield return new WaitForSeconds(0.4f);
        door.GetComponent<Animator>().SetTrigger("Open");
        opened = true;
    }
}
