using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
}

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance { get { return _instance; } }

    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);

            }
        }


    }

    public GameObject GetPooledObject(string name)
    {
        //use a for loop to iterate through the list of game objects
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //check to see if the item in the list is currently active or not (this is how we know if we're using it or not)
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name.Contains(name))
            {
                //we want to return the first non-active gameObject
                return pooledObjects[i];
            }
        }

        //if we don't have one, so if shouldExpand is true, make a new one and return that, otherwise return null
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.name.Contains(name))
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);

                    return obj;
                }
            }
        }

        return null;


    }
}
