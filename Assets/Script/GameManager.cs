using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    //유니티 배열 ,랜덤
    public string[] enemyType; // 적
    public Transform[] spawnPoint;// 위치
    double spawnDelay;
    public double nextSpawnDelay;

    public GameObject player;
    public int life; // 목숨
    public Image[] lifeImage;
    public GameObject gameover;
    GameObject enemy;
    Enemy enemyLogic;
    Rigidbody2D rigidEnemy;

    string[] fileName;

    //int indSpawn, indPos;
    //스포닝 변수들
    public List<SpawnFile> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    string line;

    public bool isPlayerImmune; // 플레이어 무적체크
    public SpriteRenderer playerRender; // 플레이어 무적 이펙트
    Color playerColor; //플레이어 무적 이펙트 2
    PlayerControl playerCon;
    PlayerControl playerLogic; // 플레이어컨트롤 스크립트 받아옴

    Coroutine runningImmune; // 이뮨변수 하나만 만들기

    void Awake()
    {
        life = 3;
        playerLogic = player.GetComponent<PlayerControl>();
        playerRender = player.GetComponent<SpriteRenderer>();
        playerCon = player.GetComponent<PlayerControl>();
        playerColor = playerRender.color;
        isPlayerImmune = false;
        runningImmune = null;

        spawnList = new List<SpawnFile>();
        enemyType = new string[] { "enemyS", "enemyM", "enemyL" };
        fileName = new string[] { "stage 1", "stage 2" };
        ReadSpawnFile(1);
    }


    void ReadSpawnFile(int fileNum)
    {
        //초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;
        //텍스트파일 읽기
        TextAsset text1 = Resources.Load(fileName[fileNum]) as TextAsset;
        StringReader str = new StringReader(text1.text);

        while(str!=null)
        {
            //텍스트파일 읽기2
            line = str.ReadLine();
            if (line == null)
                break;

            //읽은 값 리스트에 넣기
            SpawnFile spawnData = new SpawnFile();
            spawnData.delay = float.Parse(line.Split('/')[0]);
            spawnData.type = line.Split('/')[1];
            spawnData.point = int.Parse(line.Split('/')[2]);
            spawnList.Add(spawnData);
        }
        //파일 닫기
        str.Close();
        nextSpawnDelay = spawnList[0].delay;

        //1번 스폰 딜레이 적용
        
    }



    // Update is called once per frames
    void Update()
    {
        if(spawnEnd)
            ReadSpawnFile(Random.Range(0, 2));
        if (spawnDelay > nextSpawnDelay && !spawnEnd) // 스폰 딜레이가 일정 넘어서면 소환하기
        {
            //Debug.Log(spawnList.delay+" "+);
            Spawn();
            spawnDelay = 0;
        }
        Reload();
    }

    void Spawn()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
        }

        SpawnEnemy2();

        //*********기존 랜덤생성*********//
        //indPos = Random.Range(0, 9);
        //indSpawn = Random.Range(0, 3);
        //switch (indSpawn) {
        //    case 0:
        //        StartCoroutine("SmallEnemy");  // 코루틴
        //        break;
        //    case 1:
        //    case 2:
        //    SpawnEnemy2();
        //    break;
        //}

        //리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex==spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        //리스폰 딜레이 초기화
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    void SpawnEnemy2()
    {
        enemy = ObjectManager.instance.MakeObj("enemy"+spawnList[spawnIndex].type);
        enemy.transform.position = spawnPoint[spawnList[spawnIndex].point].position;  //~~라는 게임오브젝트에 복제를 합니다
        enemy.transform.rotation = Quaternion.Euler(0,0,0);
        rigidEnemy = enemy.GetComponent<Rigidbody2D>();
        enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        EnemyMove(enemy, spawnList[spawnIndex].point);
    }


    void Reload() // curShotdelay 체크함
    {
        spawnDelay += Time.deltaTime; // Time.deltaTime : 시간 단위
    }

    void EnemyMove(GameObject enemy, int pos)
    {
        //적을 받음, 적의 소환위치 받음
        switch(pos)
        {
            case 5:
            case 6:
                rigidEnemy.AddForce(new Vector2(1, -1) * enemyLogic.speed,ForceMode2D.Impulse);
                enemy.transform.Rotate(new Vector3(0, 0, 90));
                break;
            case 7:
            case 8:
                rigidEnemy.AddForce(new Vector2(-1, -1) * enemyLogic.speed, ForceMode2D.Impulse);
                enemy.transform.Rotate(new Vector3(0, 0, -90));
                break;
            default:
                rigidEnemy.AddForce(Vector2.down * enemyLogic.speed, ForceMode2D.Impulse);
                break;
        }
       
    }

    void LifeImgCheck()
    {
        for (int index = 2; index > life - 1; index--)                 // 라이프이미지 처리
            lifeImage[index].color = new Color(1, 1, 1, 0);
    }

    public void RespawnPlayer()
    {
        StartCoroutine("Respawn");

    }

    public IEnumerator Respawn()
    {
        life--;
        LifeImgCheck();
        player.SetActive(false);
        yield return new WaitForSeconds(2f);
        player.transform.position = Vector3.down * 3;
        player.GetComponent<PlayerControl>().hp = 20;
        player.SetActive(true);
        if (runningImmune == null)
        {
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
