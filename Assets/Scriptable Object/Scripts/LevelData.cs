using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
	public WaveData[] waves;

	public float timeBetweenWaves = 2f;
}