using UnityEngine;

public class LoadingEntry : MonoBehaviour
{
	public void OnClick()
	{
		SharedUI.Instance.loadingPanel.LoadScene("Home");
	}
}