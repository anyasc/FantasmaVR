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
    [SerializeField] private Weather weather;
    [SerializeField] FrontDoor doorSound;

    [SerializeField] private RainSoundControl rain;
    FMOD.Studio.EventInstance FalaInicial, FalaFinal;

    void Start()
    {
        FalaInicial = FMODUnity.RuntimeManager.CreateInstance("event:/FalasFantasma/VoceNaoEhSusana");
        FalaFinal = FMODUnity.RuntimeManager.CreateInstance("event:/FalasFantasma/FalaFinal");
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FalaInicial, transform);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FalaFinal, transform);

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

    public void CallFinalScene()
    {
        player.ableMove = false;

        StartCoroutine(FinalScene());
    }
    IEnumerator OpeningScene()
    {
        Transform finalPosition = positions[0];
        float moveTime = 1.5f;

        rain.GoInside(0.3f);
        FalaInicial.start();

        Vector3 deltaPos = (finalPosition.position - transform.position) / moveTime;
        float timePassed = 0;
        while (timePassed < moveTime)
        {
            timePassed += Time.deltaTime;
            transform.position += deltaPos * Time.deltaTime;
            yield return null;
        }
        transform.position = finalPosition.position;
        yield return new WaitForSeconds(2f);


        ReleaseWateringCan();
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("JumpForward");
        yield return new WaitForSeconds(0.25f);


        finalPosition = positions[1];
        moveTime = 1.5f;

        rain.GoInside(1f);
        deltaPos = (finalPosition.position - transform.position) / moveTime;
        timePassed = 0;
        while (timePassed < moveTime)
        {
            timePassed += Time.deltaTime;
            transform.position += deltaPos * Time.deltaTime;
            yield return null;
        }
        player.ableMove = true;

        frontDoor.SetBool("Open", false);
        yield return new WaitForSeconds(0.5f);
        doorSound.CloseSound();
        
        gameObject.SetActive(false);
    }

    IEnumerator FinalScene()
    {
        animator.SetTrigger("Idle");
        transform.rotation = positions[2].rotation;
        transform.position = positions[2].position;
        FalaFinal.start();

        Transform finalPosition = positions[3];
        float moveTime = 1f;

        Vector3 deltaPos = (finalPosition.position - transform.position) / moveTime;
        float timePassed = 0;
        while (timePassed < moveTime)
        {
            timePassed += Time.deltaTime;
            transform.position += deltaPos * Time.deltaTime;
            yield return null;
        }
        transform.position = finalPosition.position;


        yield return new WaitForSeconds(2f);

        
        finalPosition = positions[4];
        moveTime = 3f;
        
        
        deltaPos = (finalPosition.position - transform.position) / moveTime;
        timePassed = 0;
        while (timePassed < moveTime)
        {
            timePassed += Time.deltaTime;
            transform.position += deltaPos * Time.deltaTime;
            yield return null;
        }
        transform.position = finalPosition.position;
        weather.StopRain();
        frontDoor.SetBool("Open", true);
        doorSound.OpenSound();

        player.enabled = true;
        player.ableMove = true;
        Destroy(gameObject);
    }

}
