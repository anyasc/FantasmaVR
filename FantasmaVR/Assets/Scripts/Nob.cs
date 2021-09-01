using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nob : MonoBehaviour
{
    Transform door;
    public bool test = false;
    void Start()
    {
        door = transform.parent;
    }

    void Update()
    {
        if (test)
        {
            transform.parent.GetComponent<OpenDoor>().Open(transform);
            test = false;
        }
    }


}
