using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Goblin Data")]
    public GoblinScriptableObject goblinData;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY);
        if (movement.sqrMagnitude > 1)
            movement = movement.normalized;

        // Инвертируем спрайт по вертикали (flip по X) при движении влево/вправо
        if (moveX != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = moveX > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        float speed = goblinData != null ? goblinData.moveSpeed : 5f;
        rb.linearVelocity = movement * speed;
    }
}
