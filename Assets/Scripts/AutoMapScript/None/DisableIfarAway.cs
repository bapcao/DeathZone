using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject itemActivatorObject;
    private ItemActivator activatorScript;

    void Start()
    {
        itemActivatorObject = GameObject.Find("ItemActivatorObject");
        activatorScript = itemActivatorObject.GetComponent<ItemActivator>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        activatorScript.activatorItems.Add(new ActivatorItem { item = this.gameObject, itemPos = transform. position});
    }
    
}
