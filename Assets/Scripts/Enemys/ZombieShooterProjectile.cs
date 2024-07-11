using System.Collections;
using UnityEngine;

public class ZombileShooterProjectile : MonoBehaviour
{
    private ObjectPool _pool;
    private Rigidbody _rb; 
    [SerializeField] float _projectileForce;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(ObjectPool pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterTime(5f)); // 5 saniye sonra havuza geri d�n
    }

    private IEnumerator ReturnToPoolAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            ReturnToPool();
        }

    }

    public void ReturnToPool()
    {
        _rb.velocity = Vector3.zero; // Merminin h�z�n� s�f�rla
        _rb.angularVelocity = Vector3.zero; // Merminin a��sal h�z�n� s�f�rla
        _pool.ReturnToPool(transform);
    }

    public void Throw(Vector3 direction)
    {
        _rb.AddForce(direction * _projectileForce, ForceMode.Impulse);
    }
}
