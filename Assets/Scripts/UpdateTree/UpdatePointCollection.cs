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
        if (other.tag == "CollectibleWeapon")
        {
            //clickiable a� kapa
            //GunManager.mountedGuns i�inde var m� yok mu kontrol et
            other.gameObject.SetActive(false);
        }
    }

}
