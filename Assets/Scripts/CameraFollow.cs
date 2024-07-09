using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef
    public float smoothSpeed = 0.125f; // Lerp i�in h�z fakt�r�
    public Vector3 offset; // Kameran�n hedefe olan offset'i

    void Start()
    {
        // �zometrik bak�� a��s� i�in offset belirle
        offset = new Vector3(10, 10, -10);
        // Kameran�n ba�lang�� rotasyonunu hedefe do�ru bakacak �ekilde ayarla
        transform.rotation = Quaternion.Euler(30, 45, 0); // 30 derece yukar�dan, 45 derece yandan
    }

    void LateUpdate()
    {
        // �stenilen pozisyonu hesapla
        Vector3 desiredPosition = target.position + offset;
        // Mevcut pozisyon ile istenilen pozisyon aras�nda lerp uygula
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Kameray� yeni pozisyona ta��
        transform.position = smoothedPosition;

        // Kameran�n hedefe bakmas�n� sa�la
        transform.LookAt(target);
    }
}
