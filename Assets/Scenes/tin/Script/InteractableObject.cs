using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
     public string ItemName;
     public bool playerInRange;
 
    public string GetItemName()
    {
        return ItemName;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange )
        {
            if(!InventorySystem.Instance.isFull)
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full");
            }
    
        }
      
    }




    private void OTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OTriggerExit(Collider other)
    {
 if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
