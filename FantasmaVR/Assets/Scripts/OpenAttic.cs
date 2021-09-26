using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAttic : MonoBehaviour
{
    FMOD.Studio.EventInstance OpenSound;
    [SerializeField] [FMODUnity.EventRef] private string source;
    [SerializeField] private Animator atticDoor;
    public bool focus;

    private void Start()
    {
        OpenSound = FMODUnity.RuntimeManager.CreateInstance(source);
    }
    void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            StartCoroutine(Open());
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

    IEnumerator Open()
    {
        atticDoor.SetTrigger("Open");
        OpenSound.start();
        yield return new WaitForSeconds(3f);
        GetComponent<GhostLines>().Play();
        Destroy(gameObject);
    }
}
