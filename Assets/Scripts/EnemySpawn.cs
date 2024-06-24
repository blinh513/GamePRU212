using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawn : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();
    //[SerializeField]
    //public List<bool> keyCheckActive=new List<bool>();

    
    public GameObject enemyPrefabs;


    private GameObject enemy;

    private float spawnTimer = 0f;

    [SerializeField]
    private float spawnInterval = 5f;

    [SerializeField]
    private float spawnRadius = 2f;

    [SerializeField]
    private int numEnemySpa = 0;
    
    private List<GameObject> listEnemyClone=new List<GameObject>();

    Dictionary<bool, GameObject> listEnemyandKey;
    void Start()
    {

        int backCount = transform.childCount;
        for (int i = 0; i < backCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i).gameObject.transform);
        }

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            //keyCheckActive.Add(false);
            enemy = Instantiate(enemyPrefabs, transform.position, Quaternion.identity);
            enemy.SetActive(false);
            //enemy.name= i.ToString();
            listEnemyClone.Add(enemy);
            //listEnemyClone[i].name= i.ToString();
        }
        //InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }
    private void Update()
    {
        

        spawnTimer += Time.deltaTime;
        //setActive();
        if (spawnTimer >= spawnInterval && numEnemyAccept())
        {
            
            spawnTimer = 0f;

            //Vector3 randomSpawnPoint = GetRandomSpawnPoint();

            int index;
            index = Random.Range(0, spawnPoints.Count-1);
            while (listEnemyClone.ElementAt(index).activeSelf)
            {
                index = Random.Range(0, spawnPoints.Count-1);
            }

            Transform randomSpawnTransform = spawnPoints[index];
            //keyCheckActive[index] = true;
            //Vector3 randomSpawnPosition = randomSpawnTransform.position + Random.insideUnitSphere * spawnRadius;
            Vector3 randomSpawnPosition = randomSpawnTransform.position + Random.insideUnitSphere * spawnRadius;
            randomSpawnPosition.y = randomSpawnTransform.position.y;
            randomSpawnPosition.z = 0;
            listEnemyClone[index].transform.position = randomSpawnPosition;
            listEnemyClone[index].SetActive(true);
            listEnemyClone[index].GetComponent<EnemyMovement>().createHealthEnemy();
            //Instantiate(listEnemyClone[index], randomSpawnPosition, Quaternion.identity);
        }
        
    }

    Vector3 GetRandomSpawnPoint()
    {
        int index;
        index = Random.Range(0, spawnPoints.Count - 1);
        while (listEnemyClone.ElementAt(index).activeSelf)
        {
            index = Random.Range(0, spawnPoints.Count - 1);
        } 
        
        Transform randomSpawnTransform = spawnPoints[index];
        //keyCheckActive[index] = true;
        //Vector3 randomSpawnPosition = randomSpawnTransform.position + Random.insideUnitSphere * spawnRadius;
        Vector3 randomSpawnPosition = randomSpawnTransform.position +Random.insideUnitSphere * spawnRadius;
        randomSpawnPosition.y = randomSpawnTransform.position.y;
        randomSpawnPosition.z = 0;
        
        return randomSpawnPosition;
    }

    private bool emptyPosition()
    {
        //return listEnemyClone.Contains(enemy.activeSelf);
        foreach(GameObject ene in listEnemyClone)
        {
            if ( !ene.activeSelf) {
            return true;
            }
        }
        return false;
    }

    private bool numEnemyAccept()
    {
        int count = 0;
        foreach (GameObject ene in listEnemyClone)
        {
            if (ene.activeSelf)
            {
                count++;
            }
        }
        return count!=numEnemySpa;
    }

    //public void setActive()
    //{
    //    Debug.Log("Có vào setActive()"+ keyCheckActive.Count);
    //    for(int i=0;i< keyCheckActive.Count;i++)
    //    {
    //            keyCheckActive[i]= listEnemyClone[i].activeSelf;
    //        Debug.Log(i+"Có vào setActive()");
    //    }
    //}

}
