using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoor : MonoBehaviour
{

    public GameObject carpet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carpet.GetComponent<Carpet>().enabled = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carpet.GetComponent<Carpet>().enabled = true;
        }
    }
}
