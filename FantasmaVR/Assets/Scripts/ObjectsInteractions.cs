using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteractions : MonoBehaviour
{
    bool holding = false;
    public bool focus = false;
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
                switch (gameObject.tag)
                {
                    case "Object":
                        player.Grab(rb); // Metodo do script Player
                        holding = true; // Objeto passa a estar segurado, para poder soltar sem estar em foco
                        break;
                    //case "Lamp":
                    //    lightBulb.GetComponent<FlickeringLight>().lightSwitch(); // Chama metodo de ligar/desligar da luz em questao
                    //    break;
                }
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
}
