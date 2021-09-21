using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
    private Transform cam;
    private CharacterController controller;
    [SerializeField] private Transform hold, examine;
    [SerializeField] private GameObject pointer;
    [SerializeField] private Avisos aviso;
    [SerializeField] private GhostAnimations ghost;
    [SerializeField] private GameObject rain;


    public GameObject firstMenuItem;

    private Rigidbody obj; // Objeto carregado

    public float speed = 5f;
    public float strength = 5f;
    public float rotateSpeed = 5f;

    public bool ableMove = true;
    public bool menuOpened = false;
    private bool carrying = false;


    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        cam = transform.Find("MainCamera");
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    private void Update()
    {
        if (!carrying & Input.GetButtonDown("C")) // Se nao estiver segurando nada e apertar C, abrir menu
        {
            if (inventory.GetItemList().Count > 0)
            {
                OpenMenu();
            }
        }
        if (ableMove)
        {
            Walk(); // Se mover caso possa
        }

    }

    //private void FixedUpdate()
    //{
    //    if (carrying)
    //    {
    //        Rotate();
    //    }
    //}

    private void OpenMenu()
    {
        // uiInventory.gameObject.SetActive(!uiInventory.gameObject.activeSelf); // Alterna entre estados do menu
        menuOpened = !menuOpened;
        uiInventory.active = menuOpened;
        ableMove = !menuOpened; // Se menu esta aberto nao pode andar
        uiInventory.UpdateActiveItem(0);
        if (!menuOpened)
        {
            uiInventory.CloseInventory();
        }
        Debug.Log(inventory.GetItemList().Count);
    }

    private void Walk()
    {
        Vector3 velocity = (cam.right * CheckInput().x + cam.forward * CheckInput().y) * speed;
        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y = -5;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private Vector2 CheckInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        return new Vector2(x, y);
    }

    public void Grab(Rigidbody rbObj)
    {
        rbObj.transform.SetPositionAndRotation(hold.position, cam.rotation); // Posicionado no ponto hold, virado para a camera
        rbObj.transform.parent = cam; // Seguir a camera
        // Impedir rigidbody de afetar movimentos
        rbObj.useGravity = false;
        rbObj.isKinematic = true;

        carrying = true; // Player esta segurando um objeto
        obj = rbObj; // Define objeto segurado
        pointer.SetActive(false); // Desativa pointer enquanto segura objeto
    }

    public void Release()
    {
        // Redefine caracteristicas do objeto 
        obj.useGravity = true;
        obj.isKinematic = false;
        obj.transform.parent = null;

        Vector3 throwDirection = cam.forward * strength + 0.1f * strength * cam.up; // Define direcao para jogar objeto
        obj.AddForce(throwDirection, ForceMode.Impulse); // Joga objeto

        // Redefine caracteristicas do player
        carrying = false;
        obj = null;
        pointer.SetActive(true);
    }

    private void Rotate()
    {
        if (Input.GetButtonDown("C"))
        {
            ableMove = false;
            obj.transform.position = examine.position;
        }
        if (!ableMove & Input.GetButtonUp("C"))
        {
            ableMove = true;
            obj.transform.SetPositionAndRotation(hold.position, cam.rotation);
        }
        if (Input.GetButton("C"))
        {
            Quaternion deltaRotation = Quaternion.Euler(CheckInput().y * rotateSpeed, -CheckInput().x * rotateSpeed, 0);
            obj.MoveRotation(obj.rotation * deltaRotation);
        }
    }

    public void CollectItem(Item item)
    {
        inventory.AddItem(item);

    }


    public void Key(Item item)
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Open>().focus)
            {
                if (door.GetComponent<Open>().locked)
                {
                    door.GetComponent<Open>().Unlock();
                    Message("Abriu!");
                    inventory.RemoveItem(item);
                    StartCoroutine(DelayCloseMenu());
                }
                else
                {
                    Message("Essa porta já está destrancada");
                    StartCoroutine(DelayCloseMenu());
                }

            }
        }
    }

    public void Crowbar(Item item)
    {
        Downstairs basement = GameObject.Find("PassagemPorao").GetComponent<Downstairs>();
        if (basement.focus)
        {
            basement.Open();
            Message("Consegui! Abriu!");
            StartCoroutine(DelayCloseMenu());

            inventory.RemoveItem(item);
        }
        else
        {
            Message("Melhor eu tomar cuidado com isso");
        }
    }

    public void Brush(Item item)
    {

    }

    public void Photo(Item item, int amount)
    {
        JigsawPuzzle puzzle = GameObject.Find("Jigsaw").GetComponent<JigsawPuzzle>();
        if (puzzle.focus)
        {
            if (amount < 4)
            {
                Message("Acho que encaixa, mas parece ainda faltar pedaços", 5);
            }
            else
            {
                puzzle.BeginPuzzle();
                inventory.RemoveItem(item);

            }
        }
        else
        {
            Message("Não sei o que fazer com isso...", 4);
        }

    }

    public void Lighter(Item item)
    {
        Candles candles = GameObject.Find("casticalBebe").GetComponent<Candles>();
        if (candles.focus)
        {
            candles.LightCandles();
            StartCoroutine(DelayCloseMenu());

            inventory.RemoveItem(item);
        }

        else
        {
            Message("Melhor eu tomar cuidado com isso");
        }
    }


    public void Message(string s, int time = 2)
    {
        aviso.ShowText(s, time);
    }

    IEnumerator DelayCloseMenu()
    {
        yield return new WaitForSeconds(0.2f);
        OpenMenu();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EnterTrigger")
        {
            rain.SetActive(false);
            ghost.PlayOpeningScene();
            Destroy(other);
        }
    }
}