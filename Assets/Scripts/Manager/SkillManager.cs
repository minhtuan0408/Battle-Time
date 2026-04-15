using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public static SkillManager Instance;

	[SerializeField] private List<SkillSO> allSkills;

	private void Awake()
	{
		Instance = this;
	}

	public List<SkillSO> GetRandomSkills(int count)
	{
		List<SkillSO> result = new();
		List<SkillSO> temp = new(allSkills);

		for (int i = 0; i < count; i++)
		{
			if (temp.Count == 0) break;

			int index = Random.Range(0, temp.Count);
			result.Add(temp[index]);
			temp.RemoveAt(index);
		}

		return result;
	}
}