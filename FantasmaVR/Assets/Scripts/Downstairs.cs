using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downstairs : MonoBehaviour
{
    public Transform door, basementPosition, upstairsPosition;
    public bool covered = true;
    public bool opened, focus;
    public bool inBasement = false;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (focus && !covered && Input.GetButtonDown("Z"))
        {
            if (opened)
            {
                Teleport();
            }
            else
            {
                player.Message("Está emperrado... Não consigo abrir");
            }
        }
    }

    public void Open()
    {
        door.GetComponent<Animator>().SetTrigger("Open");
        opened = true;
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
}
