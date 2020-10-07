using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{


    public float speed;
    public float power;
    public int hp = 20;
    public int score = 0;

    Vector2 curPos, nextPos;
    bool isUpTouch;
    bool isDownTouch;
    bool isLeftTouch;
    bool isRightTouch;

    string[] bulletP = { "bulletPlayerA", "bulletPlayerB", "bulletPlayerC" };
    float curShotDelay; // 총 쏘고 지난 시간
    public float maxDelay; // 설정해준 후딜레이
    public Text scoreOn;

    public GameManager manager;
    Animator anim;

    public bool isPlayerImmune; // 플레이어 무적체크
    void Awake()
    {
        anim = GetComponent<Animator>();
        isPlayerImmune = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        Reload();
        TextUp();
    }

    void Move()
    {
        curPos = transform.position;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((isLeftTouch && h== -1) || (isRightTouch && h == 1))
            h = 0;
        if ((isUpTouch && v == 1) || (isDownTouch && v== -1))
            v = 0;

        nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
        
        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            anim.SetInteger("Hor", (int)h);
    }
    void Shoot()
    {
        if (!Input.GetKey(KeyCode.Space))
            return;
        if (curShotDelay<maxDelay)
            return;
        switch (power)
        {
            case 1:
                BulletMake(0, bulletP[0],0);
                break;
            case 2:
                BulletMake(0.15f, bulletP[0],0);
                BulletMake(-0.15f, bulletP[0],0);
                break;
            case 3:
                BulletMake(0.2f, bulletP[0],0);
                BulletMake(0, bulletP[1],0);
                BulletMake(-0.2f, bulletP[0],0);
                break;
            case 4:
                BulletMake(0, bulletP[0], 30);
                BulletMake(0.3f, bulletP[1], 0);
                BulletMake(0, bulletP[1], 0);
                BulletMake(-0.3f, bulletP[1], 0);
                BulletMake(0, bulletP[0], -30);
                break;
            case 5:
                BulletMake(0.6f, bulletP[0], 50);
                BulletMake(0.5f, bulletP[1], 40);
                BulletMake(0.4f, bulletP[0], 30);
                BulletMake(0, bulletP[2], 0);
                BulletMake(-0.4f, bulletP[0], -30);
                BulletMake(-0.5f, bulletP[1], -40);
                BulletMake(-0.6f, bulletP[0], -50);
                break;
        }
    } // 파워 3에서 가운데가 파랗게 나가는 기능을 만들어라!

    void BulletMake(float posMove,string bulletKind,float angle)
    {
        Vector2 bulletPos = new Vector2(transform.position.x+posMove, transform.position.y);
        GameObject bullet = ObjectManager.instance.MakeObj(bulletKind);
        bullet.transform.position = bulletPos;
        bullet.transform.Rotate(0,0,angle); // 물체 angle만큼 회전합시다
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(Quaternion.Euler(0, 0, angle) * Vector2.up * 10, ForceMode2D.Impulse); 
        curShotDelay = 0;
    }



    void Reload() // curShotdelay 체크함
    {
        curShotDelay += Time.deltaTime; // Time.deltaTime : 시간 단위
    }

    void TextUp()
    {
        scoreOn.text = score.ToString(); 
    }

    public void CheckHp() // 피가 0 이하인지 체크 // 체력에 영향을 주는 코드에서 호출 
    {
        if (hp <= 0) manager.RespawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Up":
                    isUpTouch = true;
                    break;
                case "Down":
                    isDownTouch = true;
                    break;
                case "Left":
                    isLeftTouch = true;
                    break;
                case "Right":
                    isRightTouch = true;
                    break;
            }
        }
        if (collision.gameObject.tag == "BadObject" && !isPlayerImmune)
        {
            collision.gameObject.GetComponent<AffectingObject>().AffectObject(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Up":
                    isUpTouch = false;
                    break;
                case "Down":
                    isDownTouch = false;
                    break;
                case "Left":
                    isLeftTouch = false;
                    break;
                case "Right":
                    isRightTouch = false;
                    break;
            }
        }
    }





}

