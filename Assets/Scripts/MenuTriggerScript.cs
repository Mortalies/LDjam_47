using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTriggerScript : MonoBehaviour
{
    public GameObject gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger");
        gameController.GetComponent<MenuControllerScript>().SpawnBackGround();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
