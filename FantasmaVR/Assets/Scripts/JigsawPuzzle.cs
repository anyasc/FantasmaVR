using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPuzzle : MonoBehaviour
{
    private List<Transform> pieceList;
    private Transform activePiece;
    private Player player;
    public bool puzzleActive, focus;
    [SerializeField] private GameObject pointer;
    public UI_Inventory uiInventory;
    public SpriteRenderer leftPiece;
    [SerializeField] private GhostAnimations ghost;
   

    private bool puzzleStarted = false;

    private int activeNum = 0;
    public float speed = 0.5f;
    float minDist = 0.05f;

    public Color nonActiveColor;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        pieceList = new List<Transform>();
        foreach (Transform child in transform)
        {
            pieceList.Add(child);
        }
        activePiece = pieceList[activeNum];
        nonActiveColor = activePiece.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        if (puzzleStarted)
        {
            if (focus && Input.GetButtonDown("Z"))
            {
                BeginPuzzle();
            }
        }
        if (puzzleActive)
        {
            SolvePuzzle();
            if (Check())
            {
                Complete();
            }
            if (Input.GetButtonDown("C"))
            {
                PausePuzzle();
            }
        }

    }

    public void BeginPuzzle()
    {
        puzzleActive = true;
        pointer.SetActive(false);
        player.enabled = false;
        activePiece.GetComponent<SpriteRenderer>().color = Color.white;
        foreach(Transform p in pieceList)
        {
            p.gameObject.SetActive(true);
        }
        uiInventory.CloseInventory();
        uiInventory.active = false;
        puzzleStarted = true;
    }

    private void PausePuzzle()
    {
        puzzleActive = false;
        pointer.SetActive(true);
        player.enabled = true;
        activePiece.GetComponent<SpriteRenderer>().color = nonActiveColor;
    }

    private void SolvePuzzle()
    {
        if (Input.GetButtonDown("Z"))
        {
            activePiece.GetComponent<SpriteRenderer>().color = nonActiveColor;
            if (activeNum < 3)
            {
                activeNum++;
            }
            else
            {
                activeNum = 0;
            }
            activePiece = pieceList[activeNum];

            activePiece.GetComponent<SpriteRenderer>().color = Color.white;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 velocity = (activePiece.up * v - activePiece.right * h);
        activePiece.Translate(velocity * speed * Time.deltaTime);
    }

    private bool Check()
    {
        float totalDistance = 0;
        foreach (Transform piece in pieceList)
        {
            totalDistance += Vector3.Distance(piece.localPosition, piece.GetComponent<PuzzlePiece>().correctPosition);

        }
        if (totalDistance <= minDist)
        {
            return true;
        }
        else
        {

            return false;
        }
    }

    private void Complete()
    {
        foreach (Transform piece in pieceList)
        {
            piece.localPosition = piece.GetComponent<PuzzlePiece>().correctPosition;
            piece.GetComponent<SpriteRenderer>().color = Color.white;
        }
        leftPiece.color = Color.white;
        ghost.gameObject.SetActive(true);
        ghost.CallFinalScene();

        puzzleStarted = false;
        puzzleActive = false;
        pointer.SetActive(true);
        player.enabled = true;
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
}
