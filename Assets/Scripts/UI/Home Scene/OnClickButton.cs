using UnityEngine;

public class OnClickButton : MonoBehaviour
{
	public GameObject target;

	public void Toggle()
	{
		target.SetActive(!target.activeSelf);
		Debug.Log("CLICK");
	}
}