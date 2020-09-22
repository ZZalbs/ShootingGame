using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBullet_Damage : Bullet // 나쁜 캐릭터들이 대상에 데미지를 주는 총알 공격 // player에 한정
{
    [HideInInspector]
    public new string type = "bad_bullet_damage";
    public override void AffectObject(GameObject target) // AffectingObject의 함수를 오버라이딩 // 대상에 영향을 주는 코드
    {
        base.AffectObject(target);
        //if(target.tag == "player")
        target.GetComponent<PlayerControl>().hp -= dmg; // 데미지 주고
        target.GetComponent<PlayerControl>().CheckHp(); // 상태확인함
    }
}
