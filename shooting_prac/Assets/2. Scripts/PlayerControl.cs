using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{


    public float speed;
    public float maxShotDelay; // 딜레이 제한하는 변수
    public float curShotDelay; // 현재 딜레이 계산하는 변수
    public float power;

    Vector2 curPos,nextPos;
    public bool isTopTouch;
    public bool isBottomTouch;
    public bool isLeftTouch;
    public bool isRightTouch;

    public GameObject bulletPlayerA;
    public GameObject bulletPlayerB;
    public GameManager manager;

    public bool isPlayerImmune; // 플레이어 무적체크

    
    public int score = 0; // 점수
    

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        isPlayerImmune = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        curPos = transform.position;
        float h = Input.GetAxisRaw("Horizontal");
        if ((isLeftTouch && h == -1) || (isRightTouch && h == 1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTopTouch && v == 1) || (isBottomTouch && v == -1))
            v = 0;
        nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            anim.SetInteger("Hor", (int)h);
    }

    void Fire()
    {
        if (!Input.GetKey(KeyCode.Space))
            return;
        if (curShotDelay < maxShotDelay)
            return;

        switch(power)
        {
            case 1:
                BulletShot(0,bulletPlayerA);
                break;
            case 2:
                BulletShot(1,bulletPlayerA);
                BulletShot(-1,bulletPlayerA);
                break;
            case 3:
                BulletShot(0, bulletPlayerB);
                BulletShot(2.5f,bulletPlayerA);
                BulletShot(-2.5f,bulletPlayerA);
                break;
        }
        

        curShotDelay = 0;
    }

    void BulletShot(float pos,GameObject kind)
    {
        Vector2 bulletPos = new Vector2(transform.position.x + pos * 0.1f, transform.position.y);

        GameObject bullet = Instantiate(kind, bulletPos, transform.rotation) ;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
    

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTopTouch = true;
                    break;
                case "Bottom":
                    isBottomTouch = true;
                    break;
                case "Right":
                    isRightTouch = true;
                    break;
                case "Left":
                    isLeftTouch = true;
                    break;
            }
        }

        if (collision.gameObject.tag == "Enemy"&&!isPlayerImmune)
        {
            gameObject.SetActive(false);
            manager.RespawnPlayer();
        }

        if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "boom":
                    break;
                case "power":
                    power++;
                    break;
                case "coin":
                    score += 500;
                    break;
            }

        }
    }




    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTopTouch = false;
                    break;
                case "Bottom":
                    isBottomTouch = false;
                    break;
                case "Right":
                    isRightTouch = false;
                    break;
                case "Left":
                    isLeftTouch = false;
                    break;
            }
        }
    }


}
