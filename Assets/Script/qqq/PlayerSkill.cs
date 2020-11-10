using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public int HealPointIncrease; // 단계 값을 저장, 0은 없음, 1~
    public int FireDelayDecrease;
    public int FirePowerIncrease;
    public int MoveSpeedIncrease;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void SetSkillLevel(string skill, int level)
    {
        switch(skill)
        {
            case "HealPointIncrease":
                HealPointIncrease = level;
                break;
            case "FireDelayDecrease":
                FireDelayDecrease = level;
                break;
        }
    }
}
