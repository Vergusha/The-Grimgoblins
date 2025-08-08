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

        if (!gameObject.CompareTag("Player"))
        {
            KillCounter counter = FindFirstObjectByType<KillCounter>();
            if (counter != null)
                counter.AddKill();
            Destroy(gameObject);
        }
        else
        {
            EndPanelController panel = FindFirstObjectByType<EndPanelController>();
            if (panel != null)
                panel.ShowPanel();
            gameObject.SetActive(false);
        }
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
