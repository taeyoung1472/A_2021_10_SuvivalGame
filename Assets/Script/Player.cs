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

    }
    private void FixedUpdate()
    {
        if (character.isGrounded == true)
        {
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir.z = Input.GetAxis("Vertical");
            moveDir.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = curJumpSpeed;
            }
            moveDir = this.transform.TransformDirection(moveDir);
        }
        moveDir.y -= 9.8f;
        character.Move(moveDir * Time.deltaTime * curSpeed);
    }
}
