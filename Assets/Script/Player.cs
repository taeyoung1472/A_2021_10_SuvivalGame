using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float curSpeed;
    private float CurjumpSpeed;
    bool isCrouch;
    [SerializeField] private float JumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float Runspeed;
    [SerializeField] private CharacterController character;
    [SerializeField] private Vector3 moveDir;
    void Start()
    {   
        curSpeed = speed;
    }
    void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {

    }
    void Move()
    {
        /*if (character.isGrounded == true)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir.y = 0;
            moveDir = this.transform.TransformDirection(moveDir);
        }
        moveDir.y -= 9.8f * Time.deltaTime;
        character.Move(moveDir * Time.deltaTime * speed);*/
        if (character.isGrounded)
        {
            if (isCrouch == false)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    curSpeed = Runspeed;
                else
                    curSpeed = speed;
            }
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            if (Input.GetKey(KeyCode.Space))
                moveDir.y = 3;
        }
        moveDir.y -= 9.8f * Time.deltaTime;
        character.Move(moveDir * speed * Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * lookSensitivity, 0f, Space.World);//Y위치 변경, 마우스 X 받아오기
        //CameraRotation();
        //if (Input.GetKeyDown(KeyCode.F))
        //    CheckRay();
    }
}
