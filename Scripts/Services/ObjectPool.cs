using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : GenericSingleton<ObjectPool>
{
   [System.Serializable]
   public class ItemsToPool
    {
        public GameObject objectPrefab;
        public int amountToPool;
        public bool needExpand;
    }
    public List<ItemsToPool> pooledItems = new List<ItemsToPool>();
    public List<GameObject> pooledObjects = new List<GameObject>();
    private void Start()
    {
        foreach (ItemsToPool item in pooledItems)                            //Get the items from all types of pools and add them in the list and disable them
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectPrefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
    public GameObject GetPooledObject(string tag)                          //get the required gameobject from the pool by using tag
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ItemsToPool item in pooledItems)                           //if the objects are less in the pool , increase objects
        {
            if (item.objectPrefab.tag == tag)
            {
                if (item.needExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectPrefab);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
    public void DeActivateAll()                                          //de activates all the objects
    {
        foreach (GameObject item in pooledObjects)
        {
            if (item.activeInHierarchy)
            {
                item.gameObject.SetActive(false);
                item.transform.position = Vector2.zero;
            }
        }
    }
}
