using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // Assign this in the Inspector
    public float fadeDuration = 1.0f; // Duration of the fade

    void Start()
    {
        // Ensure the fade image is fully transparent at the start
        SetAlpha(0);
    }

    public void FadeToBlack()
    {
        StartCoroutine(Fade(0, 1, fadeDuration, "PlayerWinScene"));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration, string sceneName)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(endAlpha);

        // Load the win scene after fading to black
        SceneManager.LoadScene(sceneName);
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
