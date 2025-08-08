using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObjects/Goblin")]
public class GoblinsScriptableObjects : ScriptableObject
{
	public string goblinName;
	public int hp;
	public float moveSpeed;
	public int damage;
	public float attackCooldown;
}
