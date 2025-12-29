using UnityEngine;

public class randomColor : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color[] colors;

    private void Start()
    {
        sprite.color = colors[Random.Range(0, colors.Length)];
    }
}
