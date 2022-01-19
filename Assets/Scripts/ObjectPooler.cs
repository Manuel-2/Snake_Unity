using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectsToPull
{
    public GameObject prefabToPool;
    public int amountToPool;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;

    [SerializeField] List<ObjectsToPull> itemsToPool;
    List<GameObject> instanciatedObjects;
    [SerializeField] GameObject pool;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        instanciatedObjects = new List<GameObject>();
        foreach (var item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject objectItem = Instantiate(item.prefabToPool);
                objectItem.SetActive(false);
                objectItem.transform.SetParent(pool.transform);
                instanciatedObjects.Add(objectItem);
            }
        }
    }

    public GameObject GetItem(string poolTag, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < instanciatedObjects.Count; i++)
        {
            if (!instanciatedObjects[i].activeInHierarchy && instanciatedObjects[i].GetComponent<PoolItem>().poolTag == poolTag)
            {
                instanciatedObjects[i].transform.position = position;
                instanciatedObjects[i].transform.rotation = rotation;
                instanciatedObjects[i].SetActive(true);
                return instanciatedObjects[i];
            }
        }
        foreach (ObjectsToPull item in itemsToPool)
        {
            if (item.prefabToPool.GetComponent<PoolItem>().poolTag == poolTag)
            {
                GameObject objectItem = Instantiate(item.prefabToPool, position, rotation);
                instanciatedObjects.Add(objectItem);
                objectItem.transform.SetParent(pool.transform);
                objectItem.SetActive(true);
                return objectItem;
            }
        }
        return null;
    }

    public void ReturnItem(GameObject item)
    {
        item.SetActive(false);
    }
}
