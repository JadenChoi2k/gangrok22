using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogoController : MonoBehaviour
{
    public Image logoImage;        // �ΰ� �̹���
    public GameObject targetObject; // Ȱ��ȭ�� ��� ������Ʈ
    public float fadeDuration = 1.0f; // ���̵� �ƿ� ���� �ð�
    public float displayDuration = 2.0f; // �ΰ�� ����� ��� ���� �ð�

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
        // ����� ���
        audioSource.Play();

        // �ΰ� �̹��� 2�� ���� ǥ��
        yield return new WaitForSeconds(displayDuration);

        // ���̵� �ƿ�
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

        // ��� ������Ʈ Ȱ��ȭ
        targetObject.SetActive(true);
    }
}
