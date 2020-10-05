using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnerScript : MonoBehaviour
{
    public GameObject[] trashPlastic;
    public GameObject[] trashPaper;
    public GameObject[] trashMetal;
    public GameObject[] trashGlass;
    [Header("Виды спавнящихся объектов")]
    public bool plastic = false;
    public bool paper = false;
    public bool metal = false;
    public bool glass = false;

    
    List<GameObject> trashPrefab = new List<GameObject>();
    public void SpawnObj()
    {
        CreateTrashPrefab();
        GameObject trash = Instantiate(trashPrefab[Random.Range(0, trashPrefab.Count - 1)], transform.position, transform.rotation);
    }
    void CreateTrashPrefab()
    {
        if (plastic)
        {
            foreach(GameObject trash in trashPlastic)
            {
                trashPrefab.Add(trash);
            }
        }
        if (paper)
        {
            foreach (GameObject trash in trashPaper)
            {
                trashPrefab.Add(trash);
            }
        }
        if (metal)
        {
            foreach (GameObject trash in trashMetal)
            {
                trashPrefab.Add(trash);
            }
        }
        if (glass)
        {
            foreach (GameObject trash in trashGlass)
            {
                trashPrefab.Add(trash);
            }
        }
    }
}
