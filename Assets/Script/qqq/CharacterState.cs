﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    //적과 플레이어의 동일한 상태
    public bool isStunned;
    public bool isSlowed;
<<<<<<< HEAD

    public List<float> SlowAmountList;
    public float SlowAmount;

    PlayerControl pc;
    CharacterStat cs;

    private void Start()
    {
        pc = GetComponent<PlayerControl>();
        cs = GetComponent<CharacterStat>();
    }

    private void Update()
    {
        CheckMaxAmount();
    }

    //효과
    public void AffectSlow(float amount, float duration)
    {
        StartCoroutine(AffectSlowCoolControl(amount, duration));
    }
    IEnumerator AffectSlowCoolControl(float amount, float duration)
    {
        float slowCurCool = duration;
        SlowAmountList.Add(amount);
        while(slowCurCool >= 0)
        {
            slowCurCool -= Time.deltaTime;
            yield return null;
        }
        SlowAmountList.Remove(amount);
    }

    //최대 효과 적용
    void CheckMaxAmount()
    {
        SlowAmount = Mathf.Max(SlowAmountList.ToArray());
        pc.speed = cs.MoveSpeed * (SlowAmount / 100);
    }
}
=======
}
>>>>>>> e5ca340bc0cf8c56d6a46f780c90cab3ff4817cd
