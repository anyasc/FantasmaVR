using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{
    FMOD.Studio.EventInstance Matches;
    public bool focus;
    [SerializeField] private GameObject puzzlePiece;


    private void Start()
    {
        Matches = FMODUnity.RuntimeManager.CreateInstance("event:/Matches");
    }
    public void LightCandles()
    {
        StartCoroutine(Light());
    }

    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
    }

    IEnumerator Light()
    {
        Matches.start();
        yield return new WaitForSeconds(0.6f);
        foreach (Transform candle in transform)
        {
            candle.GetChild(0).gameObject.SetActive(true);
        }
        puzzlePiece.SetActive(true);
    }
}
