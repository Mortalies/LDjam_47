using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    //объект разного материала должен иметь разные тэги
    //это префаб одно типа материала
    public enum ObjectType
    { Object0, Object1, Object2 };
    public ObjectType myObjectType = ObjectType.Object0; //тип объекта: бутылка, чашка и тд
    public GameObject[] objectsTypesPrefabs;
    private GameObject joinPoint; //объект на игроке, куда будет крепится объект
    private bool joinedToPlayer;
    

    private void Start()
    {
        joinedToPlayer = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<CharacterScript>().ReturnJoinedObject() == null)
            {
                joinPoint = collision.gameObject.transform.Find("JoinPoint").gameObject;
                joinedToPlayer = true;
                transform.GetComponent<Collider2D>().enabled = false;
                collision.gameObject.GetComponent<CharacterScript>().ChangeJoinedObject(gameObject);
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
            transform.position = joinPoint.transform.position;
        }
    }
    public void DetachObject()
    {
        joinedToPlayer = false;
        transform.GetComponent<Collider2D>().enabled = true;
        joinPoint.GetComponentInParent<CharacterScript>().ChangeJoinedObject(null);
        transform.position = transform.position + (joinPoint.transform.position - joinPoint.transform.parent.position) * 1f;//перстановка объекта полсе отпускания, чтобы он сразу опять не прилип к игроку

    }
}
