using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteractions : MonoBehaviour
{
    bool holding = false;
    public bool focus = false;
    Rigidbody rb;
    public PlayerMovement playerMovement;
    public GameObject lightBulb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (holding && Input.GetButtonDown("Grab"))
        {
            playerMovement.Release(transform, rb); // Metodo do script PlayerMovement
            holding = false; // Objeto nao esta mais sento seguardo
        }

        if (focus && Input.GetButtonDown("Grab"))
        {
            switch (gameObject.tag)
            {
                case "Object":
                    playerMovement.Grab(transform, rb); // Metodo do script PlayerMovement
                    holding = true; // Objeto passa a estar segurado, para poder soltar sem estar em foco
                    break;
                case "Lamp":
                    lightBulb.GetComponent<FlickeringLight>().lightSwitch(); // Chama metodo de ligar/desligar da luz em questao
                    break;

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
