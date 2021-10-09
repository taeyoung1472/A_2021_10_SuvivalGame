using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    RaycastHit hit;
    int bullet;
    bool isZoom;
    bool isShoot;
    bool isReloading;
    float timer;
    [SerializeField] private int gunIndex;
    [SerializeField] private AudioSource[] shoot;
    [SerializeField] private Animator[] anim;
    [SerializeField] private Animator[] anim_Zoom;
    [SerializeField] private float[] delay;
    [SerializeField] private int[] magazinBullet;
    [SerializeField] private Text bulletUI;
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform player;
    void Start()
    {
        bullet = magazinBullet[gunIndex];
        bulletUI.text = string.Format("{0} / {1}", bullet, magazinBullet);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (isZoom)
            {
                anim_Zoom[gunIndex].Play("UnZoom");
                isZoom = false;
            }
            else
            {
                anim_Zoom[gunIndex].Play("Zoom");
                isZoom = true;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && timer >= delay[gunIndex] && bullet > 0 && !isReloading)
        {
            anim[gunIndex].Play("Shoot");
            shoot[gunIndex].Play();
            timer = 0;
            bullet--;
            bulletUI.text = string.Format("{0} / {1}", bullet, magazinBullet);
            //anim.Play("Shoot");
            if (Physics.Raycast(firePos.position,firePos.forward,out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Zombie>().Damaged(10);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        anim[gunIndex].Play("ReLoad");
        isReloading = true;
        yield return new WaitForSeconds(1);
        isReloading = false;
        bullet = magazinBullet[gunIndex];
        bulletUI.text = string.Format("{0} / {1}", bullet, magazinBullet[gunIndex]);
    }
}
