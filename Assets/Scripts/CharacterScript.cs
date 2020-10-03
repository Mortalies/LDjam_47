using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("Передвижение")]
    public float speed = 4f;
    public float jumpForce = 1f;
    [Header("Взаимодействие")]
    private GameObject joinPoint;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        joinPoint = transform.Find("JoinPoint").gameObject;
        startJoinPointLocalPosition = joinPoint.transform.localPosition;
    }
    private void FixedUpdate()
    {
        velocityX = -(tempPos.x - transform.position.x) / Time.fixedDeltaTime;
        velocityY = - Mathf.Round((tempPos.y - transform.position.y) / Time.fixedDeltaTime);
        tempPos = transform.position;
        //print(velocityY);
    }
    private void Update()
    {
        CheckGround(); //проверка земли каждый кадр

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (joinedObject != null)
            {
                joinedObject.GetComponent<TrashScript>().DetachObject();
            }
        }

        AnimatorParam(); //передача парметров в аниматор контроллер
    }
    void AnimatorParam()
    {
        anim.SetFloat("speed.x", Mathf.Abs(velocityX));
        anim.SetFloat("speed.y", velocityY);
    }
    void Jump(float multiplier)
    {
            rb.velocity = transform.up * jumpForce;
    }
    void Move()
    {
        moveTemp.x = Input.GetAxis("Horizontal");
        
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
    public void ChangeJoinedObject(GameObject obj)
    {
        joinedObject = obj; 
    }
    public GameObject ReturnJoinedObject()
    {
        return joinedObject;
    }
    public void PutObjectToUrn()
    {

    }
}

