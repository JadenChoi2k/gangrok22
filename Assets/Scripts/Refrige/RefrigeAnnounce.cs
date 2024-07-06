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
            DialogMessage.Instance.AddMessage("����� ���� �����ּ���.");
            done = true;
        }
    }

    void showMessage()
    {
        DialogMessage.Instance.AddMessage("����� ���� �����ּ���.");
        done = true;
    }
}
