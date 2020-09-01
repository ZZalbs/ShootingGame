using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObj;
    public Transform[] spawnPoint;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public int life; // 목숨
    public Image[] lifeImage;
    public GameObject gameover;

    Enemy enemyLogic;
    GameObject enemy;
    Rigidbody2D rigidEnemy;

    int ranPos;
    int ranEnemy;

    public Text scoreText; // 점수 텍스트

    public bool isPlayerImmune; // 플레이어 무적체크
    public SpriteRenderer playerRender; // 플레이어 무적 이펙트
    Color playerColor; //플레이어 무적 이펙트 2
    PlayerControl playerCon;
    PlayerControl playerLogic; // 플레이어컨트롤 스크립트 받아옴
    



    Coroutine runningImmune; // 이뮨변수 하나만 만들기

    void Start()
    {
        life = 3;
        playerLogic = player.GetComponent<PlayerControl>();
        playerRender = player.GetComponent<SpriteRenderer>();
        playerCon = player.GetComponent<PlayerControl>();
        playerColor = playerRender.color;
        isPlayerImmune = false;
        runningImmune = null;

        for (int index = 0; index < life; index++)                 // 라이프이미지 처리
            lifeImage[index].color = new Color(1, 1, 1, 1);
        gameover.SetActive(false);  // 게임오버 이미지 초기화
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(maxSpawnDelay<curSpawnDelay)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
            maxSpawnDelay = Random.Range(1.0f,2.0f);
        }
        scoreText.text = string.Format("{0:n0}",playerLogic.score); // 세 자리마다 쉼표찍어서 출력하는 포맷

        if(life<=0)
        {
            GameOver();
        }
    }

    void SpawnEnemy() // 적 소환
    {
        ranPos = Random.Range(0, 9);
        ranEnemy = Random.Range(0, 5);
        switch (ranEnemy % 3)
        {
            case 0:
                StartCoroutine("Small");
                break;
            case 1:
            case 2:
                enemy = Instantiate(enemyObj[ranEnemy % 3], spawnPoint[ranPos].position, spawnPoint[ranPos].rotation);
                enemyLogic = enemy.GetComponent<Enemy>();
                enemyLogic.playerPlane = player;
                rigidEnemy = enemy.GetComponent<Rigidbody2D>();
                EnemyMove(enemy, ranPos);
                break;
        }
    }

    void EnemyMove(GameObject thisEnemy,int pos) // 적 위치조정
    {
        if(pos == 5 || pos == 6)
        {
            rigidEnemy.velocity = new Vector2(enemyLogic.speed, -1);
            thisEnemy.transform.Rotate(Vector3.forward*90);
        }
        else if (pos == 7 || pos == 8)
        {
            rigidEnemy.velocity = new Vector2(enemyLogic.speed * (-1), -1);
            thisEnemy.transform.Rotate(Vector3.back*90);
        }
        else 
        {
            rigidEnemy.velocity = Vector2.down * enemyLogic.speed;
        }
    }

    void LifeImgCheck()
    {
        for(int index=2;index>life-1;index--)                 // 라이프이미지 처리
            lifeImage[index].color = new Color(1, 1, 1, 0);
    }


    private IEnumerator Small()
    {
        for (int i = 0; i < 3; i++)
        {
            enemy = Instantiate(enemyObj[ranEnemy % 3], spawnPoint[ranPos].position, spawnPoint[ranPos].rotation);
            enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.playerPlane = player;
            rigidEnemy = enemy.GetComponent<Rigidbody2D>();
            EnemyMove(enemy, ranPos);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayer2");
        
    }

    public IEnumerator RespawnPlayer2()
    {
        life--;
        LifeImgCheck();
        yield return new WaitForSeconds(2f);
        player.transform.position = Vector3.down * 3;
        player.SetActive(true);
        if (runningImmune == null) {
            runningImmune = StartCoroutine(ImmuneMode());
        }
    }

    private IEnumerator ImmuneMode()
    {
        playerCon.isPlayerImmune = true;
        for (int i = 0; i < 7; i++)
        {
            if (i % 2 == 1)
            {
                playerColor = new Color(playerColor.r, playerColor.g, playerColor.b, 0.3f);
                playerRender.color = playerColor;
            }
            else
            {
                playerColor = new Color(playerColor.r, playerColor.g, playerColor.b, 1f);
                playerRender.color = playerColor;
            }
            yield return new WaitForSeconds(0.3f);
        }
        playerCon.isPlayerImmune = false;
        runningImmune = null;
    }


    public void GameOver()
    {
        gameover.SetActive(true);
       // Time.timeScale = 0.0f;
    }
    public void Retry()
    {
       // Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        
    }
}
