using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;

    private bool pickupActivated;

    private RaycastHit hit;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Text actionText;
    [SerializeField] private Inventory theInventory;
    void Update()
    {
        TryAction();
        CheckItem();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if(hit.transform != null)
            {
                Debug.Log(hit.transform.GetComponent<ItemPickUp>().item.itemName + "»πµÊ«ﬂΩ¿¥œ¥Ÿ");
                theInventory.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
                Destroy(hit.transform.gameObject);
                InfoDisAppear();
            }
        }
    }
    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, range, layerMask)){
            if (hit.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
            else
            {
                InfoDisAppear();
            }
        }
        else
        {
            InfoDisAppear();
        }
    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hit.transform.GetComponent<ItemPickUp>().item.itemName + " »πµÊ" + "<Color=yellow>" + " (E)" + "</color>";
    }
    private void InfoDisAppear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
