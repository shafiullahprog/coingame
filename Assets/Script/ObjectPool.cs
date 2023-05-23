using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject coins,obstacles,shield;
    public Vector2 diableObject;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;

    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            int randomNumber = Random.Range(0,2);
            if (randomNumber == 0) 
            {
                tmp = Instantiate(coins);
            }
            else
            {
                tmp = Instantiate(obstacles);
            }
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject enableObj = pooledObjects[Random.Range(0, pooledObjects.Count)];
        if (!enableObj.activeInHierarchy)
        {
            return enableObj;
        }
        else
            return null;
        /*for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
                //return pooledObjects[Random.Range()];
            }
        }
        return null;*/
    }

    public void Update()
    {
        DisableObject();
    }
    public void DisableObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].transform.position.y <= diableObject.y)
            { 
                 pooledObjects[i].SetActive(false);
            }
        }
    }
}
