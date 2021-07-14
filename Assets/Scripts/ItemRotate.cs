using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        // 먹어야 하는 아이템을 회전 시키는 것 회전축을 기준으로 위쪽으로 
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
