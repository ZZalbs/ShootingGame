using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AffectingObject // 영향을 주는 오브젝트 : 총알(기본은 데미지 주는거)
{
    public int dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NoBulletZone")
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            gameObject.SetActive(false);
        }
    }
}
