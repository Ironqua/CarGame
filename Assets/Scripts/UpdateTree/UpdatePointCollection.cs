using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePointCollection : MonoBehaviour
{
    public GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UpdatePoint")
        {
            canvas.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }
}
