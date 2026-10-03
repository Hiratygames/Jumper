using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }
}
