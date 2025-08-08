using UnityEngine;

public class HumanMovement : MonoBehaviour
{
	private float lastAttackTime = -999f;
	[Header("Human Settings")]
	public HumansScriptableObjects humanStats;
	public Transform player;
	public float stopDistance = 1.2f;

	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private HealthSystem healthSystem;
	private float moveSpeed;
	private int hp;
	private int damage;
	private float attackCooldown;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		healthSystem = GetComponent<HealthSystem>();

		// Автоматически ищем игрока по тегу
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		if (playerObj != null)
			player = playerObj.transform;

		if (humanStats != null)
		{
			moveSpeed = humanStats.moveSpeed;
			hp = humanStats.hp;
			damage = humanStats.damage;
			attackCooldown = humanStats.attackCooldown;
			gameObject.name = humanStats.humanName;
			if (healthSystem != null)
				healthSystem.Init(hp);
		}
	}

	private void FixedUpdate()
	{
		if (player == null || !player.gameObject.activeInHierarchy)
		{
			rb.linearVelocity = Vector2.zero;
			if (animator != null)
				animator.SetBool("isAttack", false);
			return;
		}

		float distance = Vector2.Distance(transform.position, player.position);
		Vector2 direction = (player.position - transform.position).normalized;

		if (distance > stopDistance)
		{
			rb.linearVelocity = direction * moveSpeed;
			if (animator != null)
				animator.SetBool("isAttack", false);
		}
		else
		{
			rb.linearVelocity = Vector2.zero;
			HealthSystem playerHealth = player.GetComponent<HealthSystem>();
			if (playerHealth != null && !playerHealth.isDead && player.gameObject.activeInHierarchy)
			{
				if (Time.time - lastAttackTime >= attackCooldown)
				{
					lastAttackTime = Time.time;
					playerHealth.TakeDamage(damage);
					if (animator != null)
						animator.SetBool("isAttack", true);
				}
				else
				{
					if (animator != null)
						animator.SetBool("isAttack", false);
				}
			}
			else
			{
				if (animator != null)
					animator.SetBool("isAttack", false);
			}
		}

		// Инвертирование спрайта по горизонтали
		if (spriteRenderer != null)
		{
			if (direction.x < -0.01f)
				spriteRenderer.flipX = true;
			else if (direction.x > 0.01f)
				spriteRenderer.flipX = false;
	}
}
}
