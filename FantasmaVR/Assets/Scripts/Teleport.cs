using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private bool focus;
    public string teleportScene;
    public Transform teleportLocation;

    private void Update()
    {
        if (focus & Input.GetButtonDown("Z"))
        {
            SceneManager.LoadScene(teleportScene);
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
