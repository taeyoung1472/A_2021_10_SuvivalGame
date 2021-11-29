using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private float curSpeed;
    private float CurjumpSpeed;
    float curCameraRotaiton;
    bool isCrouch;
    public float curRange;
    private float curHp;
    private float curStemina;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider steminaSlider;
    [SerializeField] private float maxStemina;
    [SerializeField] private float maxHp;
    [SerializeField] private float soundRange;
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
        StartCoroutine(CheckEnemy());
        curStemina = maxStemina;
        curHp = maxHp;
        curRange = soundRange;
        curSpeed = speed;
    }
    void Update()
    {
        Move();
        Rotation();
    }
    public void Damaged(int damage) {
        curHp -= damage;
        if (curHp <= 0)
        {
            //죽음
        }
        hpSlider.value = curHp / maxHp;
    }
    void Move()
    {
        if (character.isGrounded)
        {
            if (isCrouch == false)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if(curStemina <= 0)
                    {
                        curSpeed = speed;
                    }
                    else
                    {
                        curStemina -= Time.deltaTime;
                        curRange = soundRange * 2;
                        curSpeed = Runspeed;
                        handAnim.SetBool("IsRun", true);
                        handAnim.SetBool("IsMove", false);
                    }
                }
                else
                {
                    if(curStemina < maxStemina)
                    {
                        curStemina += Time.deltaTime * 2;
                    }
                    curRange = soundRange;
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
            {
                moveDir.y = 3;
            }
        }
        moveDir.y -= 9.8f * Time.deltaTime;
        character.Move(moveDir * curSpeed * Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * lookSensitivity, 0f, Space.World);//Y위치 변경, 마우스 X 받아오기
        steminaSlider.value = curStemina / maxStemina;
    }
    public void Rebound(float reboundX, float reboundY)
    {
        curCameraRotaiton += reboundY;
        transform.Rotate(0f, Input.GetAxis("Mouse X") * lookSensitivity + reboundX, 0f, Space.World);
    }
    public void Rotation()
    {
        curCameraRotaiton -= Input.GetAxis("Mouse Y") * lookSensitivity;
        curCameraRotaiton = Mathf.Clamp(curCameraRotaiton, cameraLimit.x, cameraLimit.y);
        camera.localEulerAngles = new Vector3(curCameraRotaiton, 0, 0);
    }
    public IEnumerator CheckEnemy()
    {
        while (true)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, curRange);
            for (int i = 0; i < cols.Length; i++)
            {
                //Debug.Log(cols[i].name);
                if (cols[i].CompareTag("Enemy"))
                {
                    cols[i].transform.GetComponent<Zombie>().Find(transform.position);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
