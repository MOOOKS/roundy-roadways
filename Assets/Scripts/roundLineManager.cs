using System.Collections;
using UnityEngine;

public class roundLineManager : MonoBehaviour
{
    public SpriteRenderer sprite;
    

    void Start()
    {
        sprite.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("car"))
        {           
            sprite.color = Color.green;           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("car"))
        {
            sprite.color = Color.white;
        }
    }

    
}
