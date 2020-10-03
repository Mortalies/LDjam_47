using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("Передвижение")]
    public float speed = 4f;
    public float jumpForce = 1f;
    

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector3 moveTemp;
    private bool doubleJump;
    private bool onGround;
    private Vector3 tempPos;
    private float velocityY;
    private float velocityX;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        velocityX = -(tempPos.x - transform.position.x) / Time.fixedDeltaTime;
        velocityY = - Mathf.Round((tempPos.y - transform.position.y) / Time.fixedDeltaTime);
        tempPos = transform.position;
        print(velocityY);
    }
    private void Update()
    {
        CheckGround();
        if (Input.GetButton("Horizontal"))
        {
            Move();
        }
        if (Input.GetButtonDown("Jump"))
        {

            if (onGround)
            {
                Jump(1);
            }
            else if (!doubleJump)
            {
                
                Jump(Mathf.Clamp(Mathf.Abs(velocityY), 1, 2));
                
                doubleJump = true;
            }
        }
        
        anim.SetFloat("speed.x", Mathf.Abs(velocityX));
        anim.SetFloat("speed.y", velocityY);
    }
    void Jump(float multiplier)
    {
            rb.velocity = transform.up * jumpForce;
            //rb.AddForce(transform.up * jumpForce * multiplier, ForceMode2D.Impulse);
    }
    void Move()
    {
        moveTemp.x = Input.GetAxis("Horizontal");
        
        if (moveTemp.x > 0.01f)
        {
            sprite.flipX = false;
        }
        else if (moveTemp.x < -0.01f)
        {
            sprite.flipX = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveTemp, speed * Time.deltaTime);
    }
    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f);
        Debug.DrawRay(transform.position, -transform.up, Color.red, 2f);
        if(hit.collider != null)
        {
            onGround = true;
            doubleJump = false;
            anim.SetBool("grounded", true);
        }
        else
        {
            onGround = false;
            anim.SetBool("grounded", false);
        }
    }
}
