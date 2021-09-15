using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{

    public Item item;
    private bool focus = false;
    private Player player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    private void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            player.CollectItem(item);
            Destroy(gameObject);
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
