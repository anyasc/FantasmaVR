using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upstairs : MonoBehaviour
{

    public Transform door, atticPosition, groudPosition;
    public bool opened, focus;
    public bool inAttic = false;


    public Player player;

    void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            Teleport();
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
        if (!inAttic)
        {
            player.transform.position = atticPosition.position;
        }
        else
        {
            player.transform.position = groudPosition.position;
        }
        inAttic = !inAttic;
    }

    IEnumerator OpenAnimationSound()
    {
        yield return new WaitForSeconds(0.4f);
        door.GetComponent<Animator>().SetTrigger("Open");
    }
}
