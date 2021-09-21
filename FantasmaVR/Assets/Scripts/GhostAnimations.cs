using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimations : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform wateringCan;
    [SerializeField] private List<Transform> positions;
    [SerializeField] private Animator frontDoor;
    private Player player;
    private GameObject inventory;

    // public bool test = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void Update()
    {
        //if (test)
        //{
        //    test = false;
        //    StartCoroutine(OpeningScene());
        //}
    }


    public void ReleaseWateringCan()
    {
        wateringCan.GetComponent<Rigidbody>().isKinematic = false;
        wateringCan.parent = null;
    }

    public void PlayOpeningScene()
    {
        player.ableMove = false;
        StartCoroutine(OpeningScene());
    }


    IEnumerator OpeningScene()
    {
        Transform finalPosition = positions[0];
        float moveTime = 1.5f;

        Vector3 deltaPos = (finalPosition.position - transform.position) / moveTime;
        float timePassed = 0;
        while (timePassed < moveTime)
        {
            timePassed += Time.deltaTime;
            transform.position += deltaPos * Time.deltaTime;
            yield return null;
        }
        transform.position = finalPosition.position;
        yield return new WaitForSeconds(0.5f);


        ReleaseWateringCan();
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("JumpForward");
        yield return new WaitForSeconds(0.25f);


        finalPosition = positions[1];
        moveTime = 1.5f;

        frontDoor.SetBool("Open", false);
        deltaPos = (finalPosition.position - transform.position) / moveTime;
        timePassed = 0;
        while (timePassed < moveTime)
        {
            timePassed += Time.deltaTime;
            transform.position += deltaPos * Time.deltaTime;
            yield return null;
        }
        player.ableMove = true;
        gameObject.SetActive(false);
    }



    // Como mover:

    //Vector3 deltaPos = (finalPosition.position - transform.position) / moveTime;
    //float timePassed = 0;
    //while (timePassed < moveTime)
    //{
    //    timePassed += Time.deltaTime;
    //    transform.position += deltaPos * Time.deltaTime;
    //    yield return null;
    //} 
    //transform.position = finalPosition.position;
    //yield return new WaitForSeconds(wait);
}
