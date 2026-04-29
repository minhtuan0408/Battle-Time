using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "LevelUpPanel/Reward")]
public class LevelUpPanelRewardSO : ScriptableObject
{
	public string skillName;
	public Sprite image;
	public string info;
	public int value;
	public LevelUpRewardType Type;
}

public enum LevelUpRewardType
{
	Gold,
	HP,
	Diamond,
	Ticket
}