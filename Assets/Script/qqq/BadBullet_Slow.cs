﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBullet_Slow : Bullet
{
    [HideInInspector]
    public new string type = "bad_bullet_slow";

    private void Update()
    {
        CheckDuration();
    }

    Coroutine cur;
    public override void AffectObject(GameObject target) // AffectingObject의 함수를 오버라이딩 // 대상에 영향을 주는 코드
    {
        base.AffectObject(target);
        //if(target.tag == "player")
        cur = StartCoroutine("SlowPlayer");
        target.GetComponent<CharacterState>().isSlowed = true;
    }
    IEnumerator SlowPlayer() { yield return 0; } 

    void CheckDuration()
    {

    }
}
