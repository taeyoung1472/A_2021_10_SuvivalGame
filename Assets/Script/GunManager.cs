using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunManager : MonoBehaviour
{
    RaycastHit hit;
    bool isZoom;
    bool isShoot;
    bool isReloading;
    float timer;
    [SerializeField] private Gun gun;
    [SerializeField] private Text bulletUI;
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform player;
    [SerializeField] private Player playerClass;
    void Start()
    {
        gun.gunInfo.curBullet = gun.gunInfo.maxMagagin;
        bulletUI.text = string.Format("{0} / {1}", gun.gunInfo.curBullet, gun.gunInfo.maxMagagin);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (isZoom)
            {
                gun.anim_Zoom.Play("UnZoom");
                isZoom = false;
            }
            else
            {
                gun.anim_Zoom.Play("Zoom");
                isZoom = true;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && timer >= gun.gunInfo.delay && gun.gunInfo.curBullet > 0 && !isReloading)
        {
            playerClass.Rebound(Random.Range(-gun.gunInfo.rebound.x , gun.gunInfo.rebound.x),-gun.gunInfo.rebound.y);
            gun.anim.Play("Shoot");
            gun.shootAudio.Play();
            timer = 0;
            gun.gunInfo.curBullet--;
            bulletUI.text = string.Format("{0} / {1}", gun.gunInfo.curBullet, gun.gunInfo.maxMagagin);
            Debug.DrawRay(firePos.position, firePos.forward * 10000, Color.green, 10);
            if (Physics.Raycast(firePos.position,firePos.forward * gun.gunInfo.range, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Zombie>().Damaged(gun.gunInfo.damage);
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
        gun.anim.Play("ReLoad");
        isReloading = true;
        yield return new WaitForSeconds(1);
        isReloading = false;
        gun.gunInfo.curBullet = gun.gunInfo.maxMagagin;
        bulletUI.text = string.Format("{0} / {1}", gun.gunInfo.curBullet, gun.gunInfo.maxMagagin);
    }
    public IEnumerator ChangeWeapone(Gun _gun , string _name)
    {
        yield return new WaitForEndOfFrame();
    }
}
