using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Avisos : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }


    public void ShowText(string s, int time)
    {
        text.SetText(s);
        StartCoroutine(EraseText(time));
    }


    IEnumerator EraseText(int time)
    {
        yield return new WaitForSeconds(time);
        text.SetText("");
    }

}
