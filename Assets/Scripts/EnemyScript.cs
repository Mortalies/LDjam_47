using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemyForceValue = 5f;
    public bool canWalk;
    public bool right;
    public float speed = 1f;
    private Vector3 distanaton;
    private Vector3 tempPos;
    float velocityX;

    Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Vector2 forceVector = collider.transform.position - transform.position + Vector3.up * 2f;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector * enemyForceValue, ForceMode2D.Impulse);
            collider.gameObject.GetComponent<CharacterScript>().StopWalking();
            print("force");
        }
        distanaton = -distanaton;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (right) 
        {
            distanaton = transform.right;
        }
        else
        {
            distanaton = -transform.right;
        }
    }
    private void FixedUpdate()
    {
        velocityX = -(tempPos.x - transform.position.x) / Time.fixedDeltaTime;
        tempPos = transform.position;

        if (canWalk)
        {
            //float distanceToDistanation = (transform.position - distanaton).magnitude;
            if(Mathf.Abs(velocityX) <= 0.3f)
            {
               // distanaton = -distanaton;
            }
            rb.MovePosition(transform.position + distanaton * speed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
