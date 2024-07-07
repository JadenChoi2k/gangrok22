using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodItem : Engazable
{
    private Canvas canvas;
    private Image canvasImage;
    public string Name;
    public int Amount;
    public float ExpirationHP; // max: 100
    bool engazing = false;

    Sprite statusSprite
    {
        get
        {
            int resourceNum = (int) ExpirationHP / 20;
            resourceNum = 5 - resourceNum;
            if (resourceNum == 0) resourceNum = 1;
            return Resources.Load<Sprite>($"Images/FoodStatus/{resourceNum}");
        }
    }

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvasImage = canvas.GetComponentInChildren<Image>();
        Debug.Log($"���� ĵ������ �޾ƿԳ� {name}, {canvas}, {canvasImage}");
        HideStatusBar();
    }

    public override void OnEngaze(Transform srcTransform)
    {
        if (!engazing)
        {
            engazing = true;
            ShowStatusBar(srcTransform);
        }
        StartCoroutine(AfterEngazing3Sec());
    }

    IEnumerator AfterEngazing3Sec()
    {
        yield return new WaitForSeconds(2.0f);
        engazing = false;
        yield return new WaitForSeconds(1.0f);
        if (!engazing)
        {
            HideStatusBar();
        }
    }

    void ShowStatusBar(Transform srcTransform)
    {
        if (canvas == null)
        {
            Debug.Log($"���� {name}�̴�. ĵ������ �����ϴ�.");
            return;
        }
        if (canvasImage == null)
        {
            Debug.Log($"���� {name}�̴�. ĵ���� �̹����� �����ϴ�.");
            return;
        }
        canvas.enabled = true;
        canvasImage.enabled = true;
        canvasImage.transform.parent = null;
        canvasImage.transform.localScale = new Vector3(3 * 0.02f, 2 * 0.02f, 2 * 0.02f);
        canvasImage.transform.position = canvas.transform.position;
        canvasImage.transform.rotation = canvas.transform.rotation;
        canvasImage.sprite = statusSprite;
        canvasImage.transform.parent = canvas.transform;
        Vector3 v = (srcTransform.position - canvas.transform.position).normalized;
        canvas.transform.position = canvas.transform.position + v * 0.08f;
        canvas.transform.LookAt(srcTransform.position);
    }

    void HideStatusBar()
    {
        canvas.enabled = false;
    }
}
