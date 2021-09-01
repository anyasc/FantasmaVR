using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    float direction = 0;

    public void Open(Transform nob)
    {
        
        if (direction == 0)
        {
            direction = nob.position.z > 0 ? 1 : -1;//Mathf.Pow(nob.position.z, 0);
            Vector3 rot = new Vector3(0f, direction * 110, 0f);
            transform.Rotate(rot);
        }
        else
        {
            Vector3 rot = new Vector3(0f, - direction * 110, 0f);
            transform.Rotate(rot);
            direction = 0;
        }
    }
}
