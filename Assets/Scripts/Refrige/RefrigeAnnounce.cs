using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeAnnounce : MonoBehaviour
{
    bool done = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"RefrigeAnnounce.OnTriggerEnter -> {other}");
        if (done) return;
        if (other.tag == "Player")
        {
            DialogMessage.Instance.AddMessage("냉장고 문을 열어주세요.");
            done = true;
        }
    }

    void showMessage()
    {
        DialogMessage.Instance.AddMessage("냉장고 문을 열어주세요.");
        done = true;
    }
}
