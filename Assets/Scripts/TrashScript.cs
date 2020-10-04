using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    //объект разного материала должен иметь разные тэги
    //это префаб одно типа материала
    //public enum ObjectType
    //{ Object0, Object1, Object2 };
    //public ObjectType myObjectType = ObjectType.Object0; //тип объекта: бутылка, чашка и тд
    //public GameObject[] objectsTypesPrefabs;
    [Header ("Звуки подбора мусора")]
    public AudioClip[] takeTrashSounds;
    private GameObject joinPoint; //объект на игроке, куда будет крепится объект
    private bool joinedToPlayer;
    GameControllerScript gameControllerScript;
    AudioSource audioSource;



    private void Start()
    {
        joinedToPlayer = false;
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        audioSource = GetComponent<AudioSource>();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            
            if (!gameControllerScript.ReturnStartTimer())
            {
                if (collision.gameObject.GetComponent<CharacterScript>().ReturnJoinedObject() == null)
                {
                    joinPoint = collision.transform.Find("empty").gameObject.transform.Find("JoinPoint").gameObject;
                    joinedToPlayer = true;
                    transform.GetComponent<Collider2D>().enabled = false;
                    collision.gameObject.GetComponent<CharacterScript>().ChangeJoinedObject(gameObject);
                    gameControllerScript.StartTimer(); //сброс таймерра
                    joinPoint.GetComponentInParent<CharacterScript>().gameObject.GetComponent<Animator>().SetBool("withObj", true);
                    PlayTrashSound();
                }
            }
            else if (gameControllerScript.ReturnObj() == gameObject)
            {
                if (collision.gameObject.GetComponent<CharacterScript>().ReturnJoinedObject() == null)
                {
                    joinPoint = collision.transform.Find("empty").gameObject.transform.Find("JoinPoint").gameObject;
                    joinedToPlayer = true;
                    transform.GetComponent<Collider2D>().enabled = false;
                    collision.gameObject.GetComponent<CharacterScript>().ChangeJoinedObject(gameObject);
                    joinPoint.GetComponentInParent<CharacterScript>().gameObject.GetComponent<Animator>().SetBool("withObj", true);
                    PlayTrashSound();
                }
            }
        }
    }
    void PlayTrashSound()
    {
        switch (tag)
        {
            case "Plastic":
                audioSource.PlayOneShot(takeTrashSounds[1], 1f);
                break;
            case "Paper":
                audioSource.PlayOneShot(takeTrashSounds[1], 1f);
                break;
            case "Glass":
                audioSource.PlayOneShot(takeTrashSounds[1], 1f);
                break;
            case "Metal":
                audioSource.PlayOneShot(takeTrashSounds[1], 1f);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (joinedToPlayer)
        {
            transform.position = joinPoint.transform.position - Vector3.forward;
        }
    }
    public void DetachObject()
    {
        joinedToPlayer = false;
        transform.GetComponent<Collider2D>().enabled = true;
        //transform.position = transform.position + (joinPoint.transform.position - joinPoint.transform.parent.position) * 1f;//перстановка объекта полсе отпускания, чтобы он сразу опять не прилип к игроку
        joinPoint.GetComponentInParent<CharacterScript>().gameObject.GetComponent<Animator>().SetBool("withObj", false);
        Invoke("ChangeJoinedToPlayer", 1f);
    }
    void ChangeJoinedToPlayer()
    {
        joinPoint.GetComponentInParent<CharacterScript>().ChangeJoinedObject(null);
    }

}
