using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour , IPointerClickHandler {
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject go_CountImage;

    private GunManager theGunManager;

    private void Start()
    {
        theGunManager = FindObjectOfType<GunManager>();
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemSprite;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);

        }
        SetColor(1);
    }
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        go_CountImage.SetActive(false);
        text_Count.text = "0";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                if(item.itemType == Item.ItemType.Equipment)
                {
                    //StartCoroutine(theGunManager.ChangeWeapone(item.weaponeTypoe, item.itemName));
                }
                else
                {
                    Debug.Log(item.itemName + " �� ����޽��ϴ�");
                    SetSlotCount(-1);
                }
            }
        }
    }
}