using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;// 현재꺼 인덱스
    public int lastIndex;//제일 밑에꺼 인덱스
    public Transform[] sprites;

    
    Vector3 curPos, nextPos; // 현위치, 다음위치
    [SerializeField]
    Vector3 backSpritePos,firstSpritePos; // 아랫배경p, 윗배경
    [SerializeField]
    float viewHeight; // 카메라 크기

    void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;        
    }

    void Update()
    {
        Move();
        Scroll();
    }

    void Move()
    {
        curPos = transform.position;
        nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

    }
    void Scroll()
    {
        if (sprites[lastIndex].position.y < (-1) * viewHeight)
        {
            //스프라이트 재사용
            backSpritePos = sprites[startIndex].localPosition;  //로컬포지션 쓰는 이유 : 포지션은 다른 오브젝트의 자식일 경우에 위치값이 계속 바뀜
            firstSpritePos = sprites[lastIndex].localPosition;
            sprites[lastIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;
            Debug.Log(viewHeight);

            //스프라이트 인덱스 정리
            int tmp = startIndex;
            startIndex = lastIndex;
            lastIndex = (tmp - 1 == -1) ? sprites.Length - 1 : tmp - 1;  // 배열범위 넘어가는값 예외처리

        }
    }
}
