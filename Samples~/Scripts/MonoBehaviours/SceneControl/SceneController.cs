using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace lnfu
{
    public class SceneController : MonoBehaviour
    {
		public event Action BeforeSceneUnload;
		public event Action AfterSceneLoad;

		public CanvasGroup fadeCanvasGroup;

		[Tooltip("淡入淡出時長")]
		public float fadeDuration = 1f;

		[Tooltip("第一個 Scene")]
		public string startingSceneName = "MainMenu";

		// 是否正在淡入/淡出
		private bool isFading;

		// 切換 Scene (有淡入淡出)
		// SceneReaction 在 Scripts/ScriptableObjects/Interaction/Reactions/ImmediateReactions/SceneReaction.cs 有定義
		public void FadeAndLoadScene(SceneReaction sceneReaction)
		{
			if (!isFading)
			{
				StartCoroutine(FadeAndSwitchScenes(sceneReaction.sceneName));
			}
		}

		private IEnumerator FadeAndSwitchScenes(string sceneName)
		{
			// 淡出
			yield return StartCoroutine(Fade(1f));

			if (BeforeSceneUnload != null)
				BeforeSceneUnload();

			// 把目前的 Scene unload
			yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

			// 開始 load 新的 Scene
			yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

			if (AfterSceneLoad != null)
				AfterSceneLoad();

			// 淡入
			yield return StartCoroutine(Fade(0f));
		}

		private IEnumerator LoadSceneAndSetActive(string sceneName)
		{
			// load 新的 Scene (疊加 multiple Scene)
			yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
			SceneManager.SetActiveScene(newlyLoadedScene);
		}

		private IEnumerator Fade(float finalAlpha)
		{
			isFading = true;
			fadeCanvasGroup.blocksRaycasts = true;

			// 先用 fadeDuration 計算 fade 的速度
			float fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - finalAlpha) / fadeDuration;
			while (!Mathf.Approximately(fadeCanvasGroup.alpha, finalAlpha))
			{
				fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
				yield return null;
			}

			isFading = false;
			fadeCanvasGroup.blocksRaycasts = false;
		}

		private IEnumerator Start()
		{
			// 關閉透明蓋住畫面
			fadeCanvasGroup.alpha = 1f;

			// 載入第一個 Scene
			yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

			// 開始淡入
			StartCoroutine(Fade(0f));
		}

    }
}
