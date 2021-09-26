using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject pointer;
    private bool lastScreen = false;
    private bool endgame = false;
    [SerializeField] List<GameObject> screens;

    private void Update()
    {
        if (Input.GetButtonDown("Z") && endgame)
        {
            if (lastScreen)
            {
                Application.Quit();
            }
            else
            {
                screens[0].SetActive(false);
                screens[1].SetActive(true);
                lastScreen = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play endgame
            screens[0].SetActive(true);
            player.enabled = false;
            pointer.SetActive(false);
            endgame = true;
        }
    }

}
