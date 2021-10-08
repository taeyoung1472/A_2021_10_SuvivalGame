using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float curSpeed;
    private float CurjumpSpeed;
    float curCameraRotaiton;
    bool isCrouch;
    [SerializeField] private Transform camera;
    [SerializeField] private Animator handAnim;
    [SerializeField] private float JumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float Runspeed;
    [SerializeField] private CharacterController character;
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private Vector2 cameraLimit;
    void Start()
    {   
        curSpeed = speed;
    }
    void Update()
    {
        Move();
        Rotation();
    }
    private void FixedUpdate()
    {

    }
    void Move()
    {
        if (character.isGrounded)
        {
            if (isCrouch == false)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    curSpeed = Runspeed;
                    handAnim.SetBool("IsRun", true);
                    handAnim.SetBool("IsMove", false);
                }
                else
                {
                    curSpeed = speed;
                    handAnim.SetBool("IsRun", false);
                    handAnim.SetBool("IsMove", true);
                }
            }
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if(moveDir.x == 0 && moveDir.z == 0)
            {
                handAnim.SetBool("IsRun", false);
                handAnim.SetBool("IsMove", false);
                handAnim.SetBool("IsIdle", true);
            }
            else
            {
                handAnim.SetBool("IsIdle", false);
            }
            moveDir = transform.TransformDirection(moveDir);
            if (Input.GetKey(KeyCode.Space))
                moveDir.y = 3;
        }
        moveDir.y -= 9.8f * Time.deltaTime;
        character.Move(moveDir * speed * Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * lookSensitivity, 0f, Space.World);//Y위치 변경, 마우스 X 받아오기
    }
    public void Rotation()
    {
        curCameraRotaiton -= Input.GetAxis("Mouse Y") * lookSensitivity;
        curCameraRotaiton = Mathf.Clamp(curCameraRotaiton, cameraLimit.x, cameraLimit.y);
        camera.localEulerAngles = new Vector3(curCameraRotaiton, 0, 0);
    }
}
