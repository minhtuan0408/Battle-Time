using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingPanel : MonoBehaviour
{
	public Slider loadingSlider;
	public TextMeshProUGUI Text_Value;
	public GameObject panel;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneRoutine(sceneName));
	}

	IEnumerator LoadSceneRoutine(string sceneName)
	{
		panel.SetActive(true);
		loadingSlider.gameObject.SetActive(true);
		loadingSlider.value = 0;
		Text_Value.text = "0%";

		AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
		op.allowSceneActivation = false;

		float displayProgress = 0f;
		while (op.progress < 0.9f)
		{
			float target = Mathf.Clamp01(op.progress / 0.9f);

			displayProgress = Mathf.Lerp(displayProgress, target, Time.deltaTime * 5f);

			loadingSlider.value = displayProgress;
			Text_Value.text = Mathf.RoundToInt(displayProgress * 100f) + "%";

			yield return null;
		}


		loadingSlider.value = 0.99f;
		Text_Value.text = "99%";


		op.allowSceneActivation = true;


		while (!op.isDone)
		{
			yield return null;
		}


		loadingSlider.value = 1f;
		Text_Value.text = "100%";

		yield return new WaitForSeconds(0.2f);

		panel.SetActive(false);
	}
}