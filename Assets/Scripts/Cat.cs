using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    [SerializeField] float jumpForce = 680.0f;
    [SerializeField] float walkForce = 30f;
    [SerializeField] float maxWalkSpeed = 2.0f;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //점프하기
        // if(Input.GetKeyDown(KeyCode.Space) && rigid2D.velocity.y ==0){ // 이중점프방지
        if(Input.GetKeyDown(KeyCode.Space)){
            rigid2D.AddForce(transform.up * jumpForce);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("JumpTrigger");
        }

        //좌우이동
        int key =0;
        if(Input.GetKey(KeyCode.RightArrow)) key=1;
        if(Input.GetKey(KeyCode.LeftArrow)) key =-1;

        //cat 속도
        float speedX = Mathf.Abs(rigid2D.velocity.x);

        // 속도 제한
        if(speedX < maxWalkSpeed){
            rigid2D.AddForce(transform.right * key * walkForce);
        }
        // 움직이는 방향에 따라 이미지 반전
        if(key !=0){
            transform.localScale = new Vector3(key,1,1);
        }

        // 플레이어 속도에 맞춰 에니메이션 속도 바꾼다.
        // animator는 자체적으로 내장변수로 speed가 있다.
        // animator.speed = speedX / 2.0f;
        if(rigid2D.velocity.y == 0){
            animator.speed = speedX / 2.0f;
        } else{
            animator.speed = 1.0f;
        }

        // 플레이어가 화면 밖으로 나가면 게임을 처음부터 다시 시작(로드)
        if(transform.position.y < -10){
            SceneManager.LoadScene("SampleScene");
        }
    }
    // 골 도착
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Flag")
        {
            // Debug.Log("게임오버");
            SceneManager.LoadScene("ClearScene");
        }
    }
}
