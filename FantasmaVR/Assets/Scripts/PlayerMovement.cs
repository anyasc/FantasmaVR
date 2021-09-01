using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform cam;
    public CharacterController controller;
    public GameObject pointer;

    public Transform hold;
    public Transform examine;

    public float speed = 5f;
    public float rotateSpeed = 5f;
    public float strength = 5f;

    public Vector3 input;
    public bool holdingObject = false;
    Rigidbody heldObj;

    private void Update()
    {
        if (holdingObject)
        {
            if (Input.GetButtonDown("Rotate"))
            {
                heldObj.transform.position = examine.position;
            }
            if (Input.GetButtonUp("Rotate"))
            {
                heldObj.transform.SetPositionAndRotation(hold.position, cam.rotation);
            }
        }
    }

    void FixedUpdate()
    {
        if (holdingObject && Input.GetButton("Rotate")) // Se está segurando objeto e observando, rodar
        {
            Quaternion deltaRotation = Quaternion.Euler(input.y * rotateSpeed, -input.x * rotateSpeed, 0);
            heldObj.MoveRotation(heldObj.rotation * deltaRotation); // testar Transform.Rotate no Update()
        }
        else // Andar
        {
            Vector3 velocity = (cam.right * input.x + cam.forward * input.y) * speed;
            velocity.y = 0;
            controller.Move(velocity * Time.deltaTime);
        }

    }

    public void MovementInput(Vector3 moveInput) // Receber entrada do botao analógico e colocar na variavel input
    {
        input = moveInput;
    }

    public void Grab(Transform obj, Rigidbody rb)
    {
        obj.SetPositionAndRotation(hold.position, cam.rotation); // Posicionado no ponto hold, virado para a camera
        obj.parent = cam; // Seguir a camera
        // Impedir rigidbody de afetar movimentos
        rb.useGravity = false;
        rb.isKinematic = true;

        holdingObject = true; // Player esta segurando um objeto
        heldObj = rb; // Define objeto segurado
        pointer.SetActive(false); // Desativa pointer enquanto segura objeto
    }


    public void Release(Transform obj, Rigidbody rb)
    {
        // Redefine caracteristicas do objeto 
        rb.useGravity = true;
        rb.isKinematic = false;
        obj.parent = null;

        Vector3 throwDirection = cam.forward * strength + 0.1f * strength * cam.up; // Define direcao para jogar objeto
        rb.AddForce(throwDirection, ForceMode.Impulse); // Joga objeto

        // Redefine caracteristicas do player
        holdingObject = false;
        heldObj = null;
        pointer.SetActive(true);
    }
}
