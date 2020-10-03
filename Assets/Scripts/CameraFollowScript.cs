using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    private GameObject player; // тут объект игрока
    private Vector3 offset;
    
    public float smooth = 5.0f;

    void Start()
    {
        
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }



    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * smooth);
        }
    }

}
