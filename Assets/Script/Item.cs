using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemSprite;
    public GameObject itemPrefab;
    public GunInfo gunInfo;
    public enum ItemType
    {
        Equipment, Used, Ingredient, ETC
    }
}
