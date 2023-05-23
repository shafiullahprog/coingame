using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinGenerator : MonoBehaviour
{
    public GameObject[] generatePos;
    public GameObject shield;
    public float timeToInstantiate;
    public float shiledTimer;
    private void Start()
    {
        StartCoroutine(EnableObjects());
        StartCoroutine(enableShiled());
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
    
    IEnumerator enableShiled()
    {
        float newTime = Random.Range(timeToInstantiate, shiledTimer);
        yield return new WaitForSeconds(newTime);
        GameObject shieldInstantaite = Instantiate(shield);
        shieldInstantaite.transform.position = generatePos[Random.Range(0, generatePos.Length)].transform.position;
        shieldInstantaite.SetActive(true);
        
        yield return new WaitForSeconds(shiledTimer);
        shieldInstantaite.SetActive(false);
        StartCoroutine(enableShiled());
    }
}
