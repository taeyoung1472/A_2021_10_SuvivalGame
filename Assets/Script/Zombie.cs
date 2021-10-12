using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private NavMeshAgent navMesh;
    public void Damaged(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Find(Transform player)
    {
        navMesh.SetDestination(player.position);
    }
}