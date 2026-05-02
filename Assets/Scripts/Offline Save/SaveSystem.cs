using UnityEngine;

public static class SaveSystem
{
	// ===== CURRENCY =====
	public static int Gold
	{
		get => PlayerPrefs.GetInt("gold", 0);
		set => PlayerPrefs.SetInt("gold", value);
	}

	public static int Diamond
	{
		get => PlayerPrefs.GetInt("diamond", 0);
		set => PlayerPrefs.SetInt("diamond", value);
	}

	public static int TicketSkin
	{
		get => PlayerPrefs.GetInt("ticketSkin", 0);
		set => PlayerPrefs.SetInt("ticketSkin", value);
	}

	// ===== CHARACTER =====
	public static bool IsCharUnlocked(int id)
	{
		return PlayerPrefs.GetInt($"char_{id}", id == 0 ? 1 : 0) == 1;
	}

	public static void UnlockChar(int id)
	{
		PlayerPrefs.SetInt($"char_{id}", 1);
	}

	// ===== SAVE =====
	public static void Save()
	{
		PlayerPrefs.Save();
	}
}