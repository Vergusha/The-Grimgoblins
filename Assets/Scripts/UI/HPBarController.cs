using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public Slider hpSlider;
    public HealthSystem playerHealth;
    [Header("UI References")]
    public GameTimer gameTimer;

    private void Start()
    {
        if (playerHealth != null && hpSlider != null)
        {
            hpSlider.maxValue = playerHealth.maxHP;
            hpSlider.value = playerHealth.GetCurrentHP();
            playerHealth.OnDeath += OnPlayerDeath;
            if (gameTimer != null)
                gameTimer.StopOnPlayerDeath(playerHealth);
        }
    }

    private void Update()
    {
        if (playerHealth != null && hpSlider != null)
        {
            hpSlider.value = Mathf.Max(0, playerHealth.GetCurrentHP());
        }
    }

    private void OnPlayerDeath()
    {
        if (hpSlider != null)
        {
            hpSlider.value = 0;
            hpSlider.gameObject.SetActive(false);
        }
    }
}
