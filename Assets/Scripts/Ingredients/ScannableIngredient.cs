using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannableIngredient : Engazable
{
    private Renderer m_renderer;
    private bool engazed = false;
    private bool scanned = false;
    BlinkingOutliner outliner;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    public override void OnEngaze()
    {
        if (engazed || scanned) return;
        engazed = true;

        Debug.Log("Scanning Started");

        outliner = FindObjectOfType<BlinkingOutliner>();
        if (outliner != null)
        {
            Debug.Log("draw outline");
            outliner.AddOutline(GetHashCode().ToString(), m_renderer);
        }
        StartCoroutine(ScanFor3To6Sec());
    }

    IEnumerator ScanFor3To6Sec()
    {
        float waitSec = Random.Range(3f, 6f);
        yield return new WaitForSeconds(waitSec);
        if (outliner != null)
        {
            // outliner.RemoveOutline(GetHashCode().ToString());
        }

        Debug.Log($"Scan for {name} is done");
        scanned = true;
    }
}
