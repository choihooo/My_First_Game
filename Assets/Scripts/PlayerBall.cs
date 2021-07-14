using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 장면 전환 매니저

public class PlayerBall : MonoBehaviour
{
    public float jumpPower; // 점프의 힘을 unity에서 바로 조정할 수 있게 public을 줬다.
    public int ItemCount; // 먹은아이템
    bool isJump; // 점프한 상태
    Rigidbody rigid;
    AudioSource audio; // 오디오
    public GameManagerLogic manager;
    void Awake() // Awake함수에서는 선언한 변수들의 초기화를 한다
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>(); // rigidbody가져옴 물리 시뮬레이션을 통해서 오브젝트의 위치를 조절합니다.
        // 리지드바디(rigidbody) 컴포넌트는 오브젝트의 위치를 제어합니다. 
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isJump == false) // 스페이스바를 누르고 점프를 하고 있지 않는 상태라면
        {
            isJump = true; // 점프하는 중으로 바꾼다
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse); // Y축 방향으로 jumpPower만큼 힘을 줘서 올린다.
        }
    }
    void FixedUpdate() // 물리적인 업데이트는 FixedUpdate에서 한다
    {
        float h = Input.GetAxisRaw("Horizontal"); // 이동을 입력 받아오는 함수 Raw가 붙으면 1값이 들어온다 
        float v = Input.GetAxisRaw("Vertical"); // 이동을 입력 받아오는 함수 Raw가 붙으면 1값이 들어온다 

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse); // 실제로 키보드를 누른 방향으로 힘을 가해서 물체를 이동시키는 함수
    }

    void OnCollisionEnter(Collision collision)  // 점프를 1단 점프로 제한하기 위해 만들었다. 
    {   // OnCollisionEnter는 이collider/ rigidbody에 다른 collider/ rigidbody가 닿을 때 호출됩니다.
        if (collision.gameObject.tag == "Floor") // Floor 태그를 가진 오브젝트와 충돌한다면
        {
            isJump = false; // 점프하는 상태를 false로 바꿈
        }
    }

    void OnTriggerEnter(Collider other) //item 먹으면 item 사라지기
    { // Find 계열 함수는 CPU의 부하를 초래할 수 있으므로 최소한의 사용만 한다.
        if (other.tag == "Item") // Item 태크를 가진 오브젝트와 만나면
        {
            ItemCount++; // 획득한 아이템의 갯수 증가
            audio.Play(); // 오디오 재생
            other.gameObject.SetActive(false); //gameObject자기 자신, other을 비활성화 시킨다.
            manager.GetItem(ItemCount); // UI표시를 위해 획득한 아이템의 갯수를 전달해준다.
        }
        else if (other.tag == "Point") // 골인지점을 포인트로 작성함
        {
            if (manager.totalItemCount == ItemCount) //모든 아이템을 먹고 골인할 경우
            {
                //clear
                SceneManager.LoadScene("stage_" + (manager.stage + 1).ToString());
            }
            else // 모든 아이템을 먹지 않고 골인할 경우
            {
                // restart
                SceneManager.LoadScene("stage_" + manager.stage.ToString());
            }
        }
    }
}

