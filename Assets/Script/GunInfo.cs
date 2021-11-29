using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gun", menuName = "New Gun/Gun")]
public class GunInfo : ScriptableObject
{
    public string weaponeName;
    public WeaponeType itemType;
    public Vector2 rebound;
    public float reloadTime;
    public int maxMagagin;
    public int curBullet;
    public int damage;
    public float delay;
    public float range;
    public Sprite weaponeSprite;
    public GameObject prefabs;
    public enum WeaponeType
    {
        Main, Serve, Melee, Special
    }
}