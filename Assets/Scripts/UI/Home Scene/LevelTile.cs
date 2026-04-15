using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelTile : MonoBehaviour, IPointerClickHandler
{
	public GameObject Lock;
	public int levelIndex; // level 1,2,3,4

	private bool isLock;

	public void Setup(bool locked)
	{
		isLock = locked;
		Lock.SetActive(locked);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (isLock) return;

		SharedUI.Instance.loadingPanel.LoadScene("GamePlay");
	}
}