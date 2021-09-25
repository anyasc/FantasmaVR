using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    FMOD.Studio.EventInstance CollectSound;
    public Item item;
    private bool focus = false;
    private Player player;
    public bool insideDrawer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        CollectSound = FMODUnity.RuntimeManager.CreateInstance("event:/GetItem");
    }
    private void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            player.CollectItem(item);
            if (insideDrawer)
            {
                transform.parent.GetComponent<Open>().enabled = true;
            }
            CollectSound.start();
            Destroy(gameObject);
        }
    }
    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
        if (insideDrawer)
        {
            transform.parent.GetComponent<Open>().enabled = false;
        }
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
        if (insideDrawer)
        {
            transform.parent.GetComponent<Open>().enabled = true;
        }

    }
}
