using UnityEngine;

public class SkinController : MonoBehaviour
{
	[Header("Reference")]
	public Animator visualAnimator;

	[Header("Skins")]
	public AnimatorOverrideController[] skins;

	int currentSkin = -1;

	public void ChangeSkin(int index)
	{
		if (index < 0 || index >= skins.Length) return;

		if (currentSkin == index) return;
		AnimatorStateInfo state = visualAnimator.GetCurrentAnimatorStateInfo(0);
		visualAnimator.runtimeAnimatorController = skins[index];
		visualAnimator.Play(state.fullPathHash, 0, state.normalizedTime);

		currentSkin = index;
	}
}