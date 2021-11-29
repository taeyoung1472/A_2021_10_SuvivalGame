using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{
    bool isDead;
    [SerializeField] private Animator anim;
    [SerializeField] private float hp;
    [SerializeField] private NavMeshAgent navMesh;
    public void Damaged(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
        {
            anim.SetBool("IsDead",true);
            StartCoroutine(WaitForDead());
            Destroy(gameObject,10f);
        }
    }
    public void Find(Vector3 playerPos)
    {
        if (!isDead)
        {
            anim.SetBool("IsMove", true);
            navMesh.SetDestination(playerPos);
        }
    }
    private IEnumerator WaitForDead()
    {
        yield return new WaitForSeconds(1f);
        isDead = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && !isDead)
        {
            anim.Play("Attack");
            collision.transform.GetComponent<Player>().Damaged(10);
        }
    }
}