using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTriggerScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.CompareTag("Player"))
        {
                GetComponentInParent<EdgeCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<EdgeCollider2D>().isTrigger = true;
        }
    }
}
