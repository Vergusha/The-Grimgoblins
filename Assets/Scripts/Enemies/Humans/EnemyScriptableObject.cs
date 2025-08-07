using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 2)]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Основные характеристики")]
    public string enemyName = "Враг";
    public Sprite enemySprite;
    public int maxHealth = 15;
    public float moveSpeed = 3f;
    public int attackPower = 3;
    public float attackRange = 1.2f;
    public float detectionRange = 6f;
    [Header("Особенности")]
    public bool isRanged;
    public bool isBoss;
    [TextArea]
    public string description;
}
