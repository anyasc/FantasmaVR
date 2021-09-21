using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{

    public bool focus = false;
    public bool opened = false;
    public bool locked = false;
    public Animator animator;
    public GameObject impedingDoor;
    public UI_Inventory uiInventory;

    private void Start()
    {
        animator = GetComponent<Animator>();
        uiInventory = GameObject.Find("UI Inventory").GetComponent<UI_Inventory>();
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
                    FindObjectOfType<AudioManager>().Play("PortaTrancada");
                    Debug.Log("porta trancada");
                }
            }
        }
    }
    public void OpenClose()
    {
        animator.SetTrigger("OpenClose");
        if (!opened)
        {
            FindObjectOfType<AudioManager>().Play("PortaAbrindo");

        }
        opened = !opened;
    }

    public bool Unlock()
    {
        if (locked)
        {
            //StartCoroutine(DelayUnlock());
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


    IEnumerator DelayUnlock()
    {
        yield return new WaitForSeconds(0.2f);
        locked = false;
    }
}
