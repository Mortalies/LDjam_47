﻿using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("Передвижение")]
    public float speed = 4f;
    public float jumpForce = 1f;
    public float jumpTimer = 0.5f;
    [Header("Взаимодействие")]
    private GameObject joinPoint;
    private bool canJump;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector3 moveTemp;
    private bool doubleJump;
    private bool onGround;
    private Vector3 tempPos;
    private float velocityY;
    private float velocityX;
    private Vector3 startJoinPointLocalPosition;
    private GameObject joinedObject;
    private bool down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        joinPoint = transform.Find("empty").gameObject.transform.Find("JoinPoint").gameObject;
        startJoinPointLocalPosition = joinPoint.transform.localPosition;
        canJump = true;
    }
    private void FixedUpdate()
    {
        velocityX = -(tempPos.x - transform.position.x) / Time.fixedDeltaTime;
        velocityY = - Mathf.Round((tempPos.y - transform.position.y) / Time.fixedDeltaTime);
        tempPos = transform.position;

        CheckGround(); //проверка земли каждый кадр

        if (Input.GetButton("Horizontal"))
        {
            Move();
        }
        //print(velocityY);
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                Jump(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (joinedObject != null)
            {
                joinedObject.GetComponent<TrashScript>().DetachObject();
            }
        }
        down = false;
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            down = true;
        }
            print(Input.GetAxisRaw("Vertical"));
        AnimatorParam(); //передача парметров в аниматор контроллер
    }
   
    void AnimatorParam()
    {
        anim.SetFloat("speed.x", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        anim.SetFloat("speed.y", velocityY);
        anim.SetBool("withObj", joinedObject);
    }
    void Jump(float multiplier)
    {
        //rb.velocity = transform.up * jumpForce;
        if (canJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            Invoke("ChangeCanJump", jumpTimer);
        }
    }
    void ChangeCanJump()
    {
        canJump = true;
    }
    void Move()
    {
        moveTemp.x = Input.GetAxisRaw("Horizontal");
        
        if (moveTemp.x > 0.01f)
        {
            sprite.flipX = false;
            joinPoint.transform.localPosition = startJoinPointLocalPosition;
        }
        else if (moveTemp.x < -0.01f)
        {
            sprite.flipX = true;
            joinPoint.transform.localPosition = new Vector3 (-startJoinPointLocalPosition.x, startJoinPointLocalPosition.y, startJoinPointLocalPosition.z);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveTemp, speed * Time.fixedDeltaTime);
        //rb.MovePosition(transform.position + moveTemp * speed / 100);
    }
    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f);
        Debug.DrawRay(transform.position, -transform.up, Color.red, 2f);
        if (hit.collider != null)
        {
            onGround = true;
            anim.SetBool("grounded", true);
        }
        else
        {
            onGround = false;
            anim.SetBool("grounded", false);
        }
        if (down)
        {
            if (hit.transform.GetComponent<LadderScript>() != null)
            {
                hit.transform.GetComponent<LadderScript>().DisableCollider();
            }
        }

    }
    public void ChangeJoinedObject(GameObject obj)
    {
        joinedObject = obj; 
    }
    public GameObject ReturnJoinedObject()
    {
        return joinedObject;
    }
    
}

