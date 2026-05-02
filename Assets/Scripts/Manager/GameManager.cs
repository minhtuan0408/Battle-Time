using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameObject[] MapPrefab;

	public int CurrentLevel{get; private set;}

	private void Start()
	{
		Instance = this;
	}
	public void SetLevel(int i) => CurrentLevel = i;
	public void AddGold(int i)
	{
		SaveSystem.Gold += i;
		SaveSystem.Save();
	}
	public void AddDiamond(int i)
	{
		SaveSystem.Diamond += i;
		SaveSystem.Save();
	}
	public void AddSkinTicket(int i)
	{
		SaveSystem.TicketSkin += i;
		SaveSystem.Save();
	}

	public int GetLevel() => CurrentLevel;
}
