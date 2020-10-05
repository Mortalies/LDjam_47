using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    [Header("Передвижение")]
    public float speed = 4f;
    public float jumpForce = 1f;
    public float jumpTimer = 0.5f;
    public float stopWalkingTimer = 3f;
    [Header("Звуки")]

    public AudioClip jumpSound;
    public AudioClip stepSoundPark;
    public float stepSoundParkVolume = 0.5f;
    public AudioClip stepSoundOffice;
    public float stepSoundOfficeVolume = 0.5f;
    public AudioClip charSound;
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
    private bool down; //проверка нажатия конпки вниз
    private bool canWalk; //застенен ли игрок
    private AudioSource audioSource;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        joinPoint = transform.Find("empty").gameObject.transform.Find("JoinPoint").gameObject;
        startJoinPointLocalPosition = joinPoint.transform.localPosition;
        canJump = true;
        canWalk = true;
        anim.SetBool("withObj", false);
    }
    private void FixedUpdate()
    {
        velocityX = -(tempPos.x - transform.position.x) / Time.fixedDeltaTime;
        velocityY = - Mathf.Round((tempPos.y - transform.position.y) / Time.fixedDeltaTime);
        tempPos = transform.position;

        CheckGround(); //проверка земли каждый кадр
        if (canWalk)
        {
            if (Input.GetButton("Horizontal"))
            {
                Move();
            }
        }
        //print(velocityY);
    }
    public void UnJoinObj()
    {
        if (joinedObject != null)
        {
            anim.SetBool("withObj", true);
            joinedObject.GetComponent<TrashScript>().DetachObject();
        }
    }
    private void Update()
    {
        if (canWalk)
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
                    anim.SetBool("withObj", true);
                    joinedObject.GetComponent<TrashScript>().DetachObject();
                }
            }
            down = false;
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                down = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(charSound);
        }
        //print(Input.GetAxisRaw("Vertical"));
        AnimatorParam(); //передача парметров в аниматор контроллер
    }
   
    void AnimatorParam()
    {
        if (canWalk)
        {
                anim.SetFloat("speed.x", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        }
        else
        {
            anim.SetFloat("speed.x", 0f);
        }
        anim.SetFloat("speed.y", velocityY);
        //anim.SetBool("withObj", joinedObject);
    }
    void Jump(float multiplier)
    {
        //rb.velocity = transform.up * jumpForce;
        if (canJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            anim.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
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
        RaycastHit2D[] hits = { 
            Physics2D.Raycast(transform.position + Vector3.right * 0f, -transform.up, 1f) };
        //hits[0] = Physics2D.Raycast(transform.position, -transform.up, 1f);

        Debug.DrawRay(transform.position + Vector3.right * 0f, -transform.up, Color.red, 1f);

        foreach (RaycastHit2D hit in hits) {
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
                if (hit.transform.GetComponent<PlatformScript>() != null)
                {
                    hit.transform.GetComponent<PlatformScript>().DisableCollider();
                }

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
    public void StopWalking()
    {
        canWalk = false;
        Invoke("StartWalking", stopWalkingTimer);

    }
    void StartWalking()
    {
        canWalk = true;
    }
    
    void StepEvent()
    {
        string levelName = SceneManager.GetActiveScene().name; 
        switch (levelName)
        {
            case "Level 1":
                audioSource.PlayOneShot(stepSoundPark, stepSoundParkVolume);
                break;
            case "Level 2":
                audioSource.PlayOneShot(stepSoundOffice, stepSoundOfficeVolume);
                break;
            case "Level 3":
                audioSource.PlayOneShot(stepSoundOffice, stepSoundOfficeVolume);
                break;
            default:
                break;
        }
    }
}

