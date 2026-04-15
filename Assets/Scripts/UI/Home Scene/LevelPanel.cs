using UnityEngine;

public class LevelPanel : MonoBehaviour
{
	public LevelTile[] tiles; 
	public GameObject content;
	void Start()
	{
		int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

		for (int i = 0; i < tiles.Length; i++)
		{
			int level = i + 1;

			bool isLocked = level > unlockedLevel;

			tiles[i].levelIndex = level;
			tiles[i].Setup(isLocked);
		}
	}
}