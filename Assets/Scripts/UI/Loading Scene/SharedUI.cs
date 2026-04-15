using UnityEngine;

public class SharedUI : MonoBehaviour
{
	public static SharedUI Instance;

	public LoadingPanel loadingPanel;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}