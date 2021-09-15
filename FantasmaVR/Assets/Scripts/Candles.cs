using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{

    public bool focus;
    [SerializeField] private GameObject puzzlePiece;

    public void LightCandles()
    {
        foreach(Transform candle in transform)
        {
            candle.GetChild(0).gameObject.SetActive(true);
        }
        puzzlePiece.SetActive(true);
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
