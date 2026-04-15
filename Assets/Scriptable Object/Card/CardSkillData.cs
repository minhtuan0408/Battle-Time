using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Skill")]
public class CardSkillData : ScriptableObject
{
	public string skillName;
	public Sprite icon;
	[TextArea] public string description;

	public int maxLevel;
}