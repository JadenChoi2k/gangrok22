using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engazer : MonoBehaviour
{
    RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("RayInteraction"))
            {
                var egz = hit.collider.GetComponent<Engazable>();
                if (egz != null) egz.OnEngaze(transform);
            }
        }
    }
}
