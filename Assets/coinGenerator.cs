using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinGenerator : MonoBehaviour
{
    public GameObject[] generatePos;
    public float timeToInstantiate;
    private void Start()
    {
        StartCoroutine(EnableObjects());
    }


    IEnumerator EnableObjects()
    {
        GameObject obstacles = ObjectPool.SharedInstance.GetPooledObject();
        if (obstacles != null)
        {
            obstacles.transform.position = generatePos[Random.Range(0,generatePos.Length)].transform.position;
            obstacles.SetActive(true);
        }
        yield return new WaitForSeconds(timeToInstantiate);
        StartCoroutine(EnableObjects());
    }    
}
