using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Goblin", order = 1)]
public class GoblinScriptableObject : ScriptableObject
{
    [Header("Основные характеристики")]
    public string goblinName = "Гоблин";
    public Sprite goblinSprite;
    public int maxHealth = 10;
    public float moveSpeed = 2f;
    public int attackPower = 2;
    public float attackRange = 1f;
    public float detectionRange = 5f;
    [Header("Особенности")]
    public bool isRanged;
    public bool isBoss;
    [TextArea]
    public string description;
}
