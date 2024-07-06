using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScannableIngredient : Engazable
{
    private Renderer m_renderer;
    private bool engazed = false;
    private bool scanned = false;

    [SerializeField]
    private Material EngazeMaterial;

    [SerializeField]
    private TMP_Text loadingText;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    public override void OnEngaze()
    {
        if (engazed || scanned) return;
        engazed = true;
        Debug.Log("Scanning Started");
        StartCoroutine(ScanFor1To3Sec());
    }

    IEnumerator ScanFor1To3Sec()
    {
        loadingText.gameObject.SetActive(true);
        m_renderer.materials = new[] { m_renderer.materials[0], EngazeMaterial };
        float waitSec = Random.Range(1f, 3f);
        float t = 0;
        while (t < waitSec)
        {
            t += Time.deltaTime;
            loadingText.text = $"{Mathf.Floor(t / waitSec * 100)}%";
            yield return null;
        }
        loadingText.text = "100%";
        yield return new WaitForSeconds(0.1f);
        loadingText.gameObject.SetActive(false);
        m_renderer.materials = new[] { m_renderer.materials[0] };

        Debug.Log($"Scan for {name} is done");
        scanned = true;
    }
}
