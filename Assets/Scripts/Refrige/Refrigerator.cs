using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    bool opening = false;
    bool rotating = false;
    bool first = true;
    [SerializeField]
    private GameObject DirtySuggest;

    static readonly Quaternion opened = Quaternion.Euler(90, 0, -93.2f);
    static readonly Quaternion closed = Quaternion.Euler(90, 0, -178f);
    Quaternion destRot => opening ? opened : closed;

    void Update()
    {
        if (!rotating) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, destRot, 0.05f);
        if (transform.rotation == destRot) rotating = false;
    }

    public void Toggle()
    {
        if (rotating) return;
        Debug.Log("Toggle!!");
        opening = !opening;
        rotating = true;
        if (first && opening)
        {
            first = false;
            StartCoroutine(ShowSuggestionAfter3Sec());
        }
    }

    IEnumerator ShowSuggestionAfter3Sec()
    {
        yield return new WaitForSeconds(3);
        DirtySuggest.SetActive(true);
    }
}
