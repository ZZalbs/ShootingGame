using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectingObject : MonoBehaviour
{
    [HideInInspector]
    public string type;

    public virtual void AffectObject(GameObject target) { } // 적이나 플레이어에 영향을 주는 코드 작성하는 부분 // 여기는 동일하게 적용하는 코드
}
