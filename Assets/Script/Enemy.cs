using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 
    public int speed;
    public int hp;
    private int fullhp;
    public int value; // 사망시 점수
    public GameObject player;// 에너미 스크립트의 플레이어 : 프리팹이라 못넣기 때문에 게임매니저에서 지정해줌
    PlayerControl control;
    public Sprite[] image = new Sprite[2];
    public string enemyName; // 이름
    public float maxShotDelay; // 딜레이 제한하는 변수
    public float curShotDelay; // 현재 딜레이 계산하는 변수
    PlayerControl playerLogic; // 플레이어컨트롤 스크립트 받아옴
    bool dieCheck = false;
    Vector3 dirVec;
    public GameObject bulletEnemyA;
    public GameObject bulletEnemyB;




    SpriteRenderer enemyRender;
    // Start is called before the first frame update
    void Start()
    {
        enemyRender = GetComponent<SpriteRenderer>();
        control = player.GetComponent<PlayerControl>();
        fullhp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
        if(hp<=0)
        {
            control.score += value;
            hp = fullhp;
            gameObject.SetActive(false);
        }
    }

    void Fire()// 공격
    {
        if (curShotDelay < maxShotDelay)
            return;

        dirVec = player.transform.position - transform.position;

        if (enemyName == "Enemy_small")
        {
            BulletShotEnemy(bulletEnemyA, dirVec, 0);
        }
        else if (enemyName == "Enemy_big")
        {
            BulletShotEnemy(bulletEnemyA, dirVec, 5);
            BulletShotEnemy(bulletEnemyA, dirVec, -5);
        }

        curShotDelay = 0;
    }

    void BulletShotEnemy(GameObject kind, Vector3 dir, int move)
    {
        if (curShotDelay < maxShotDelay)
            return;
        Vector2 bulletPos = new Vector2(transform.position.x + move * 0.1f, transform.position.y - 0.2f);

        GameObject bullet = Instantiate(kind, bulletPos, transform.rotation);
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(dir, ForceMode2D.Impulse);
    }


    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }


    void Onhit(int dmg)
    {
        if (transform.position.x > 3 || transform.position.x < -3 || transform.position.y > 5 || transform.position.y < -5)
            return;
        hp -= dmg;
        Invoke("resetSprite", 0.1f);
        enemyRender.sprite = image[1];

    }

    void resetSprite()
    {
        enemyRender.sprite = image[0];
    }
    private void OnTriggerEnter2D(Collider2D collision)
        
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            Onhit(bullet.dmg);
        }
        if (collision.gameObject.tag == "NoBulletZone")
        {
            gameObject.SetActive(false);
        }
    }

}
