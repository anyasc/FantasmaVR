using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteractions : MonoBehaviour
{
    bool holding = false;
    public bool focus = false;
    public bool insideDrawer;
    Rigidbody rb;
    public Player player;
    public GameObject lightBulb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (!player.menuOpened)
        {
            if (holding && Input.GetButtonDown("Z"))
            {
                player.Release(); // Metodo do script Player
                holding = false; // Objeto nao esta mais sento seguardo
            }

            if (focus && Input.GetButtonDown("Z"))
            {
                player.Grab(rb); // Metodo do script Player
                if (insideDrawer)
                {
                    transform.parent.GetComponent<Open>().enabled = true;
                }
                holding = true; // Objeto passa a estar segurado, para poder soltar sem estar em foco
            }
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
