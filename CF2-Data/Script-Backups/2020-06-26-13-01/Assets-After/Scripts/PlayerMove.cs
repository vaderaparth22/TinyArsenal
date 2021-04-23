using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public TimeBody TimeBody;

    public ParticleSystem dashParticles;
    public GameObject JetFlame;
    public GameObject[] ignitefrom;
    public Transform PlayerHead;


    private CharacterController characterController;
    private Camera cam;

    public float gravity = 14.0f;
    public float movementSpeed;
    public float additionalSpeed;
    public float jumpForce = 10.0f;
    public float rotationSpeed = 450f;
    public float dashEffectRate;

    public float dashSpeed;
    public float maxDashTime;
    public float dashStoppingSpeed;
    public bool isHoldingShift;

    private float verticalVelocity;
    private float currentDashTime;
    private float defaultMoveSpeed;

    private Vector3 moveVector;
    private Vector3 movementTemp;
    private Quaternion targetRotation;
    private int floorMask;


    //public Rigidbody Playerrig;
    //bool Inair;

    void Start()
    {

        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        floorMask = LayerMask.GetMask("Ground");

        defaultMoveSpeed = movementSpeed;
        currentDashTime = maxDashTime;
    }

    private void Update()
    {
        if(isHoldingShift == false)
        {
            Move_Character();
        }

        Mouse_Turning();

        //Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
        //Debug.DrawRay(JetFlame.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);

    }



    private void Move_Character()
    {

        if (!characterController.isGrounded)
        {
            verticalVelocity -= gravity * Time.unscaledDeltaTime;
            moveVector.y = verticalVelocity;
        }

        //JetPack Purpose
        //if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
        //{

        //    Vector3 JetVelocity = new Vector3(Playerrig.velocity.x,  20 , Playerrig.velocity.z);
        //    Playerrig.velocity = JetVelocity;
        //    verticalVelocity = JetVelocity.y;
        //    moveVector.y = verticalVelocity;
        //    moveVector = Vector3.zero;
        //    moveVector.y = verticalVelocity;
        //    GameObject Flame = Instantiate(JetFlame, ignitefrom[0].transform.position, Quaternion.identity);
        //    Flame.transform.rotation = Quaternion.Euler(180, Flame.transform.rotation.y, Flame.transform.rotation.z);
        //    Flame.transform.SetParent(ignitefrom[0].transform);
        //    GameObject Flame1 = Instantiate(JetFlame, ignitefrom[1].transform.position, Quaternion.identity);
        //    Flame1.transform.rotation = Quaternion.Euler(180, Flame1.transform.rotation.y, Flame1.transform.rotation.z);
        //    Flame1.transform.SetParent(ignitefrom[1].transform);

        //}
        //else
        //{
        //    moveVector = Vector3.zero;
        //moveVector.y = verticalVelocity;
        //}


        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space))
        {
            currentDashTime = 0.0f;
        }

        if (currentDashTime < maxDashTime)
        {
            currentDashTime += dashStoppingSpeed;

            var em = dashParticles.emission;
            em.rateOverDistance = dashEffectRate;

            moveVector.x = ControlFreak2.CF2Input.GetAxisRaw("Horizontal") * dashSpeed;
            moveVector.z = ControlFreak2.CF2Input.GetAxisRaw("Vertical") * dashSpeed;

            characterController.Move(moveVector * Time.unscaledDeltaTime);
        }
        else
        {
            var em = dashParticles.emission;
            em.rateOverDistance = 0f;

            moveVector.x = ControlFreak2.CF2Input.GetAxis("Horizontal") * movementSpeed;
            moveVector.z = ControlFreak2.CF2Input.GetAxis("Vertical") * movementSpeed;

            characterController.Move(moveVector * Time.unscaledDeltaTime);
        }
    }

    private void Mouse_Turning()
    {

        Ray camRay = Camera.main.ScreenPointToRay(ControlFreak2.CF2Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 100f, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            PlayerHead.rotation = newRotation;
        }
    }

    private void Keys_Turning()
    {
        if (movementTemp != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(movementTemp);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.unscaledDeltaTime);
        }
    }
}
