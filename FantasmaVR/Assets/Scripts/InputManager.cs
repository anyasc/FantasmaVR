using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    Vector3 input;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    void Update()
    {

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        playerMovement.MovementInput(input);
    }
}
