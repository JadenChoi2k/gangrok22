using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    bool opening = false;
    bool rotating = false;

    static readonly Quaternion opened = Quaternion.Euler(90, -90, -179.568f);
    static readonly Quaternion closed = Quaternion.Euler(90, 0, -179.568f);
    Quaternion destRot => opening ? opened : closed;

    void Update()
    {
        if (!rotating) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, destRot, 0.1f);
        if (Quaternion.Angle(transform.rotation, destRot) < 1)
        {
            transform.rotation = destRot;
            rotating = false;
        }
    }
    
    public void Toggle()
    {
        if (rotating) return;
        Debug.Log("Toggle!!");
        opening = !opening;
        rotating = true;
    }
}
