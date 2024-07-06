using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogoController : MonoBehaviour
{
    public Image logoImage;        // 로고 이미지
    public GameObject targetObject; // 활성화할 대상 오브젝트
    public float fadeDuration = 1.0f; // 페이드 아웃 지속 시간
    public float displayDuration = 2.0f; // 로고와 오디오 재생 지속 시간

    private AudioSource audioSource;
    private CanvasGroup canvasGroup;

    void Start()
    {
        audioSource = logoImage.GetComponent<AudioSource>();
        canvasGroup = logoImage.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        StartCoroutine(PlayLogoSequence());
    }

    private IEnumerator PlayLogoSequence()
    {
        // 오디오 재생
        audioSource.Play();

        // 로고 이미지 2초 동안 표시
        yield return new WaitForSeconds(displayDuration);

        // 페이드 아웃
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / fadeDuration;

        float progress = 0.0f;
        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;

        // 대상 오브젝트 활성화
        targetObject.SetActive(true);
    }
}
