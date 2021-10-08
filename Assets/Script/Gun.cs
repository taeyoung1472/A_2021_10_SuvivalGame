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
    [SerializeField] private AudioSource shoot;
    [SerializeField] private Text bulletUI;
    [SerializeField] private Transform firePos;
    [SerializeField] private float delay;
    [SerializeField] private int magazinBullet;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    void Start()
    {
        bullet = magazinBullet;
        bulletUI.text = string.Format("{0} / {1}", bullet, magazinBullet);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && timer >= delay && bullet > 0 && !isReloading)
        {
            shoot.Play();
            timer = 0;
            bullet--;
            bulletUI.text = string.Format("{0} / {1}", bullet, magazinBullet);
            //anim.Play("Shoot");
            if (Physics.Raycast(firePos.position,firePos.forward,out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().Damaged(10);
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
        isReloading = true;
        yield return new WaitForSeconds(1);
        isReloading = false;
        bullet = magazinBullet;
        bulletUI.text = string.Format("{0} / {1}", bullet, magazinBullet);
    }
}
