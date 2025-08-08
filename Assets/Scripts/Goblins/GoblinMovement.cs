
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    private float lastAttackTime = -999f;
    [Header("Goblin Settings")]
    public GoblinsScriptableObjects goblinStats;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private HealthSystem healthSystem;
    private float moveSpeed;
    private int hp;
    private int damage;
    private float attackCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthSystem = GetComponent<HealthSystem>();

        if (goblinStats != null)
        {
            moveSpeed = goblinStats.moveSpeed;
            hp = goblinStats.hp;
            damage = goblinStats.damage;
            attackCooldown = goblinStats.attackCooldown;
            gameObject.name = goblinStats.goblinName;
            if (healthSystem != null)
                healthSystem.Init(hp);
        }
    }

    private void Update()
    {
        // Проверка атаки по ЛКМ с кулдауном
        if (healthSystem != null && !healthSystem.isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    if (animator != null)
                        animator.SetBool("isAttack", true);

                    // Атака по врагу через OverlapCircle
                    float attackRadius = 0.7f; // радиус удара
                    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
                    foreach (var hit in hits)
                    {
                        if (hit.gameObject != gameObject) // не атаковать себя
                        {
                            HealthSystem enemyHealth = hit.GetComponent<HealthSystem>();
                            if (enemyHealth != null)
                            {
                                enemyHealth.TakeDamage(damage);
                            }
                        }
                    }
                }
            }
            else
            {
                if (animator != null)
                    animator.SetBool("isAttack", false);
            }
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = movement * moveSpeed;

        // Управление анимацией ходьбы
        bool isWalking = movement.magnitude > 0.01f;
        if (animator != null)
        {
            animator.SetBool("isWalking", isWalking);
        }

        // Инвертирование спрайта по горизонтали
        if (spriteRenderer != null)
        {
            if (moveX < -0.01f)
                spriteRenderer.flipX = true;
            else if (moveX > 0.01f)
                spriteRenderer.flipX = false;
        }
    }
}