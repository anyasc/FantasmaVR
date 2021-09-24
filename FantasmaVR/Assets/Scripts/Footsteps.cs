using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private int materialIndex = 0;

    private float deltaDistance;
    private Vector3 lastPosition;

    public float stepDistance = 1.6f;
    private float randomAdd;



    // Start is called before the first frame update
    void Start()
    {
        randomAdd = Random.Range(0f, 0.3f);
        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        deltaDistance += (transform.position - lastPosition).magnitude;
        if (deltaDistance >= stepDistance)
        {
            MaterialCheck();
            PlayFootstep();
            randomAdd = Random.Range(0f, 0.3f);
            deltaDistance = 0f;
        }
        lastPosition = transform.position;
    }



    void PlayFootstep()
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/Footstep");
        Footstep.setParameterByName("Material", materialIndex);
        Footstep.start();
        Footstep.release();
    }

    private void MaterialCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f))
        {
            materialIndex = hit.collider.gameObject.GetComponent<FloorMaterial>() != null ? hit.collider.gameObject.GetComponent<FloorMaterial>().material : 0;
        }
        else
        {
            materialIndex = 0;
        }

    }
}
