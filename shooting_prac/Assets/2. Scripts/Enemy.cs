using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName; // 이름
    public int enemyScore; // 사망시 점수
    public Sprite[] mode;  // 피격판정
    public float speed; // 스피드
    public int hp; // 체력

    SpriteRenderer enemyRender;
    Rigidbody2D rigid;

    public GameObject bulletEnemyA;
    public GameObject bulletEnemyB;

    public float maxShotDelay; // 딜레이 제한하는 변수
    public float curShotDelay; // 현재 딜레이 계산하는 변수
    public GameObject playerPlane; // 에너미 스크립트의 플레이어 : 프리팹이라 못넣기 때문에 게임매니저에서 지정해줌
    PlayerControl playerLogic; // 플레이어컨트롤 스크립트 받아옴
    bool dieCheck = false;

    Vector3 dirVec;

    // Start is called before the first frame update
    void Start()
    {
;       rigid = GetComponent<Rigidbody2D>();
        enemyRender = GetComponent<SpriteRenderer>();
        playerLogic = playerPlane.GetComponent<PlayerControl>();



    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()// 공격
    { 
        if (curShotDelay < maxShotDelay)
            return;

        dirVec = playerPlane.transform.position - transform.position;

        if (enemyName == "Enemy_small")
        {
            BulletShotEnemy(bulletEnemyA,dirVec,0);
        }
        else if (enemyName == "Enemy_big")
        {
            BulletShotEnemy(bulletEnemyA, dirVec,5);
            BulletShotEnemy(bulletEnemyA, dirVec,-5);
        }

        curShotDelay = 0;
    }

    void BulletShotEnemy(GameObject kind,Vector3 dir,int move)
    {
        if (curShotDelay < maxShotDelay)
            return;
        Vector2 bulletPos = new Vector2(transform.position.x + move * 0.1f, transform.position.y-0.2f);

        GameObject bullet = Instantiate(kind, bulletPos, transform.rotation);
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(dir,ForceMode2D.Impulse);
    }




    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }



    void Onhit(int damage)
    {
        hp -= damage;
        enemyRender.sprite = mode[1];
        Invoke("modechange",0.1f); // 인보크 : 함수 뽑기 전 선딜레이 

        if (hp <= 0&&!dieCheck)         //사망처리
        {
            dieCheck = true;
            playerLogic.score += enemyScore;
            Destroy(gameObject);
        }
    }



    void modechange()
    {
        enemyRender.sprite = mode[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NoBulletZone")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Bullet")
        {
            Onhit(collision.gameObject.GetComponent<Bullet>().dmg);
        }
    }





}
