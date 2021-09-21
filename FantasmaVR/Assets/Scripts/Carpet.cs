using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{

    public bool focus = false;
    public Transform finalPosition;
    public float speed = 5f;
    public Downstairs downstairs;

    // Update is called once per frame
    void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            Reveal();
        }
    }

    private void Reveal()
    {

        StartCoroutine(SmoothMove());
        downstairs.covered = false;
        enabled = false;
    }


    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
    }

    IEnumerator SmoothMove()
    {

        // Getting the difference in position
        Vector3 deltaPos = finalPosition.position - transform.position;
        // Repeat once per frame for a second:
        float timePassed = 0;
        while (timePassed < 1.0f)
        {
            // Increment the time to prevent an infinite loop
            timePassed += Time.deltaTime;
            // Slowly adjust the position and rotation so it approaches the spawn position and rotation
            transform.position += deltaPos * Time.deltaTime;
            // Exit the function (it will begin where it left off the next frame)
            yield return null;
        }
        // Ensures the player is exactly where you want him when the time is up           
        transform.position = finalPosition.position;
        // Waits for a second after the player has returned before running the game again
        yield return new WaitForSeconds(1f);

    }
}
