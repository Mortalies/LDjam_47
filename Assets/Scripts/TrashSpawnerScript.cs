using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnerScript : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public int keyN = 1000;
    private void Update()
    {
        if(Random.Range(0, keyN) == 0)
        {
            GameObject trash = Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length - 1)], transform.position, transform.rotation);
        }
    }
}
