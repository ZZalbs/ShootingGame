﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBullet_Slow : Bullet
{
    [HideInInspector]
    public new string type = "bad_bullet_slow";
<<<<<<< HEAD
=======

    private void Update()
    {
        CheckDuration();
    }
>>>>>>> e5ca340bc0cf8c56d6a46f780c90cab3ff4817cd

    Coroutine cur;
    public override void AffectObject(GameObject target) // AffectingObject의 함수를 오버라이딩 // 대상에 영향을 주는 코드
    {
        base.AffectObject(target);
<<<<<<< HEAD
        //if (target.tag == "player")
        target.GetComponent<CharacterState>().AffectSlow(20,3); // 슬로우 효과
        target.GetComponent<PlayerControl>().hp -= dmg; // 데미지 주고
        target.GetComponent<PlayerControl>().CheckHp(); // 상태확인함
=======
        //if(target.tag == "player")
        cur = StartCoroutine("SlowPlayer");
        target.GetComponent<CharacterState>().isSlowed = true;
    }
    IEnumerator SlowPlayer() { yield return 0; } 

    void CheckDuration()
    {

>>>>>>> e5ca340bc0cf8c56d6a46f780c90cab3ff4817cd
    }
}
