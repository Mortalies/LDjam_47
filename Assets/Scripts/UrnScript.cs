using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrnScript : MonoBehaviour
{
    

    public enum ObjectTag
    { Plastic, Paper, Glass, Metal };
    [Header("Тег данного вида материала")]
    public ObjectTag myObjectType = ObjectTag.Plastic; //название материала
    private string tagName = "Plastic";
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        tagName = myObjectType.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        SwitchColor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            Debug.Log(tagName + " with name " + collision.gameObject.name + " was destroyed");
            Destroy(collision.gameObject);
        }
    }
    void SwitchColor()
    {
        switch (myObjectType.ToString())
        {
            case "Plastic":
                spriteRenderer.color = Color.yellow;
                break;
            case "Paper":
                spriteRenderer.color = Color.green;
                break;
            case "Glass":
                spriteRenderer.color = Color.blue;
                break;
            case "Metal":
                spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }
    }
}
