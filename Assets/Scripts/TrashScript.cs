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
    private GameObject joinPoint; //объект на игроке, куда будет крепится объект
    private bool joinedToPlayer;
    GameControllerScript gameControllerScript;
    

    private void Start()
    {
        joinedToPlayer = false;
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        
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
                }
            }
        }
    }
    
    

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    print("collision");
    //    if(collision.CompareTag("Player"))
    //    {
    //        print("collision player");
    //        joinPosition = collision.gameObject.transform.Find("JoinPoint").gameObject;
    //        joinToPlayer = true;
    //    }
    //}
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
