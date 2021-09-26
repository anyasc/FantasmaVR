using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Read : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    [SerializeField] private Vector3 readRotation;
    [SerializeField] private Vector3 readPosition;
    public Transform examine, initialParent;
    private Player player;
    public bool insideDrawer;
    [SerializeField] private bool firstTime = true;


    public bool focus, reading;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialParent = transform.parent;
        initialRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        examine = GameObject.Find("Examine").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((focus || reading) && Input.GetButtonDown("Z"))
        {
            Reading();
        }
    }


    private void Reading()
    {
        reading = !reading;
        if (reading)
        {
            if (insideDrawer)
            {
                transform.parent.GetComponent<Open>().enabled = true;
            }
            if (firstTime && GetComponent<GhostLines>() != null)
            {
                GetComponent<GhostLines>().Play();
                firstTime = false;
            }
            transform.parent = examine;
            transform.position = examine.position;
            transform.localPosition += readPosition;
            transform.localEulerAngles = readRotation;
            reading = true;
        }
        else
        {
            transform.parent = initialParent;
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            reading = false;
        }
        player.carrying = reading;
        player.pointer.SetActive(!reading);
        player.ableMove = !reading;
    }
    public void PointerEnter() // Permitir acoes com pointer no objeto
    {
        focus = true;
        if (insideDrawer)
        {
            transform.parent.GetComponent<Open>().enabled = false;
        }
    }

    public void PointerExit() // Não permitir acoes com pointer fora do objeto
    {
        focus = false;
        if (insideDrawer)
        {
            transform.parent.GetComponent<Open>().enabled = true;
        }

    }
}
