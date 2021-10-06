using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    bool isTrue;
    float curSpeed;
    [SerializeField] float speed;
    [SerializeField] private float timer;
    void Start()
    {
        StartCoroutine(Watering());
    }
    private void Update()
    {
        transform.Translate(Vector3.up * curSpeed * Time.deltaTime);
    }
    IEnumerator Watering()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            if (isTrue)
            {
                isTrue = false;
                curSpeed = speed;
            }
            else
            {
                isTrue = true;
                curSpeed = -speed;
            }

        }
    }
}
