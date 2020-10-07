using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBullet_Slow : Bullet
{
    [HideInInspector]
    public new string type = "bad_bullet_slow";

    Coroutine corSlowPlayer;
    public override void AffectObject(GameObject target) // AffectingObject의 함수를 오버라이딩 // 대상에 영향을 주는 코드
    {
        base.AffectObject(target);
        //if (target.tag == "player")
        target.GetComponent<CharacterState>().AffectSlow(20,3); // 슬로우 효과
        target.GetComponent<PlayerControl>().hp -= dmg; // 데미지 주고
        target.GetComponent<PlayerControl>().CheckHp(); // 상태확인함
    }
}
