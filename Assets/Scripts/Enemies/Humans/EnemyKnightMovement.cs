using UnityEngine;

public class EnemyKnightMovement : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyScriptableObject enemyData;
    private Rigidbody2D rb;
    private Transform player;

    public float avoidRadius = 0.7f; // Радиус для проверки других врагов
    public float avoidStrength = 2f; // Сила отталкивания

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Автоматически ищем игрока с тегом "Goblin"
        GameObject goblinObj = GameObject.FindGameObjectWithTag("Goblin");
        if (goblinObj != null)
            player = goblinObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Vector2 direction = (player.position - transform.position).normalized;

        // Логика отталкивания от других врагов
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidRadius);
        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject && col.CompareTag("Human"))
            {
                Vector2 away = (transform.position - col.transform.position).normalized;
                direction += away * avoidStrength;
            }
        }
        direction = direction.normalized;

        float speed = enemyData != null ? enemyData.moveSpeed : 3f;
        rb.linearVelocity = direction * speed;

        // Инвертируем спрайт по горизонтали только если враг движется к игроку
        Vector2 toPlayer = (player.position - transform.position).normalized;
        if (Mathf.Abs(toPlayer.x) > 0.05f)
        {
            Vector3 scale = transform.localScale;
            scale.x = toPlayer.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
