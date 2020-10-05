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
    public AudioClip[] enemySounds;

    Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<CharacterScript>().UnJoinObj();
            Vector2 forceVector = (collider.transform.position - transform.position + Vector3.up * 2f) * 0.8f;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector * enemyForceValue, ForceMode2D.Impulse);
            collider.gameObject.GetComponent<Animator>().SetTrigger("Jump");
            collider.gameObject.GetComponent<CharacterScript>().StopWalking();
            distanaton = -distanaton;
            GetComponent<AudioSource>().PlayOneShot(enemySounds[Random.Range(0, enemySounds.Length - 1)]);
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;

        } 
        if(collider.CompareTag("Plastic") || collider.CompareTag("Metal") || collider.CompareTag("Paper") || collider.CompareTag("Glass"))
        {
            print("plastic or else");
            Vector2 forceVector = (collider.transform.position - transform.position + Vector3.up * 1f) * 0.25f;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector * enemyForceValue, ForceMode2D.Impulse);
        } 
        if (collider.CompareTag("EnemyObstacle"))
        {
            distanaton = -distanaton;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (right) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
            distanaton = transform.right;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
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
