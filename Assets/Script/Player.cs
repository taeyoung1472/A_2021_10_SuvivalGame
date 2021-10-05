using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float curSpeed;
    private float jumpSpeed;
    [SerializeField] private float curJumpSpeed;
    [SerializeField] private float speed;
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
        if (character.isGrounded == true)
        {
            Debug.Log("isGrounded");
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump!");
                moveDir.y = jumpSpeed;
            }
            moveDir = this.transform.TransformDirection(moveDir);
        }
        moveDir.y -= 9.8f * Time.deltaTime;
        character.Move(moveDir * Time.deltaTime * speed);
    }
}
