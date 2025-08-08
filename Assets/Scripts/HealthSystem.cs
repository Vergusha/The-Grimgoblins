using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [HideInInspector]
    public int maxHP = 10;
    private int currentHP;
    public System.Action OnDeath;
    public bool isDead = false;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void Init(int hp)
    {
        maxHP = hp;
        currentHP = hp;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        if (OnDeath != null)
            OnDeath.Invoke();
        // Для игрока можно не удалять объект, а отключать управление и UI
        if (gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
