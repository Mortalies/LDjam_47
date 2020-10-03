using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    }

    public void DisableCollider()
    {
        GetComponent<EdgeCollider2D>().isTrigger = true;
    }
}
