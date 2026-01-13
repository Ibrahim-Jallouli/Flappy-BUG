using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;
    private Transform cachedTransform;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cachedTransform = transform;
    }

    private void OnEnable()
    {
        Vector3 position = cachedTransform.position;
        position.y = 0f;
        cachedTransform.position = position;
        direction = Vector3.zero;
        
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(AnimateSprite));
    }

    private void Update()
    {
        // Check for keyboard, mouse, or touch input
        bool jump = Keyboard.current.spaceKey.wasPressedThisFrame ||
                    Mouse.current.leftButton.wasPressedThisFrame;

        // Check for touch input (mobile)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            jump = true;
        }

        if (jump)
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        cachedTransform.position += direction * Time.deltaTime;

        Vector3 rotation = cachedTransform.eulerAngles;
        rotation.z = direction.y * tilt;
        cachedTransform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            GameManager.Instance.IncreaseScore();
        }
    }

}
