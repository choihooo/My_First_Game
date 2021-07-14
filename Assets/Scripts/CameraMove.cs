using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform; //게임오브젝트의 위치, 회전 그리고 스케일(scale)을 나타냅니다.
    Vector3 Offset; // 
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform; // Player의 위치를 알려준다.
        Offset = transform.position - playerTransform.position; // 카메라의 상대위치를 구한다.
    }
    void LateUpdate() // LateUpdate를 쓰는 이유는 카메라 이동은 다른 업데이트들이 끝나고 업데이트 되야 따라가는 것처럼 진행되기 때문이다
    {
        transform.position = playerTransform.position + Offset; // 카메라와 플레이어가 일정한 간격을 두고 떨어지게 한다.
    }
}
