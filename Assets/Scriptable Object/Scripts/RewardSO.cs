using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Reward")]
public class RewardSO : ScriptableObject
{
	public string rewardName;
	public Sprite icon;
	public int value;
}
