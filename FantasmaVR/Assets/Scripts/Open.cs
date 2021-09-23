using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    FMOD.Studio.EventInstance OpenCloseSound, LockedSound, UnlockSound;


    public bool focus = false;
    public bool opened = false;
    public bool locked = false;
    public Animator animator;
    public GameObject impedingDoor;
    public UI_Inventory uiInventory;
    private string soundEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        uiInventory = GameObject.Find("UI Inventory").GetComponent<UI_Inventory>();
        LockedSound = FMODUnity.RuntimeManager.CreateInstance("event:/LockedDoor");
        UnlockSound = FMODUnity.RuntimeManager.CreateInstance("event:/UnlockDoor");
        soundEvent =
            gameObject.CompareTag("Door") ? "event:/OpenCloseDoor" :
            gameObject.CompareTag("Drawer") ? "event:/OpenCloseDrawer" :
            null;
        if (soundEvent != null)
            OpenCloseSound = FMODUnity.RuntimeManager.CreateInstance(soundEvent);
    }
    private void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            if (!uiInventory.active)
            {
                if (!locked) // Se porta destrancada
                {
                    if (impedingDoor == null) // Se não tiver nenhuma porta que possa impedir esta de abrir
                    {
                        OpenClose();
                    }
                    else // Fechar a porta impedindo antes de abrir esta
                    {
                        if (impedingDoor.GetComponent<Open>().opened)
                        {
                            impedingDoor.GetComponent<Open>().OpenClose();
                        }
                        else
                        {
                            OpenClose();
                        }

                    }
                }
                else
                {
                    LockedSound.start();
                }
            }
        }
    }
    public void OpenClose()
    {
        animator.SetTrigger("OpenClose");
        if (soundEvent != null)
        {
            OpenCloseSound.setParameterByName("Estado", opened ? 1 : 0);
            OpenCloseSound.start();
        }
        opened = !opened;
    }

    public bool Unlock()
    {
        if (locked)
        {
            UnlockSound.start();
            locked = false;

        }
        return false;
    }



    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
    }


}
