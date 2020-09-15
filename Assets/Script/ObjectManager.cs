using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject enemySPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemyLPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletPlayerCPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject loading;                        

    GameObject[] enemyS;
    GameObject[] enemyM;
    GameObject[] enemyL;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletPlayerC;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;

    GameObject[] targetPool; // 풀링할 타겟 설정

    

    void Awake()
    {
        if(instance != this)
            instance = this;
        enemyS = new GameObject[20];
        enemyM = new GameObject[10];
        enemyL = new GameObject[10];

        bulletPlayerA = new GameObject[200];
        bulletPlayerB = new GameObject[200];
        bulletPlayerC = new GameObject[200];
        bulletEnemyA = new GameObject[200];
        bulletEnemyB = new GameObject[200];
        StartCoroutine("Generate");
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }


    IEnumerator Generate()
    {
        loading.SetActive(true);
        Time.timeScale = 0.0f;
        for (int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < bulletPlayerA.Length; i++)
        {
            bulletPlayerA[i] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < bulletPlayerB.Length; i++)
        {
            bulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < bulletPlayerC.Length; i++)
        {
            bulletPlayerC[i] = Instantiate(bulletPlayerCPrefab);
            bulletPlayerC[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < bulletEnemyA.Length; i++)
        {
            bulletEnemyA[i] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }

        for (int i = 0; i < bulletEnemyB.Length; i++)
        {
            bulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[i].SetActive(false);
            //yield return new WaitForSeconds(0.001f);
        }
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.001f);
        loading.SetActive(false);
    }


    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "enemyS" :
                targetPool = enemyS;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyL":
                targetPool = enemyL;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletPlayerC":
                targetPool = bulletPlayerC;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    


}
