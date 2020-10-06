using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    //적과 플레이어의 동일한 상태
    public bool isStunned;
    public bool isSlowed;

    public List<float> SlowAmountLIst;
    public float SlowAmount;

    PlayerControl pc;
    CharacterStat cs;
    private void Start()
    {
        pc = GetComponent<PlayerControl>();
        cs = GetComponent<CharacterStat>();
        StartCoroutine(CheckMaxAmount());
    }

    //효과
    IEnumerator AffectSlow(float amount, float duration)
    {
        float slowCurCool = duration;
        SlowAmountLIst.Add(amount);
        while(slowCurCool >= 0)
        {
            slowCurCool -= Time.deltaTime;
            yield return null;
        }
        SlowAmountLIst.Remove(amount);
    }

    IEnumerator CheckMaxAmount()
    {
        foreach(float amount in SlowAmountLIst)
        {
            if (SlowAmount < amount) SlowAmount = amount;
            yield return null;
        }
        pc.speed = cs.MoveSpeed * (SlowAmount / 100);
        //...
    }
}