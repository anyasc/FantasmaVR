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

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (focus && Input.GetButtonDown("Grab"))
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
        }
    }
    public void OpenClose()
    {
        animator.SetTrigger("OpenClose");
        opened = !opened;
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
