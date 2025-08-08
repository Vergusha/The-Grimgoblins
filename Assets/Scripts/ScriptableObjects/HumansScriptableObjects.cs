using UnityEngine;

[CreateAssetMenu(fileName = "Human", menuName = "ScriptableObjects/Human")]
public class HumansScriptableObjects : ScriptableObject
{
	public string humanName;
	public int hp;
	public float moveSpeed;
	public int damage;
	public float attackCooldown;
}
