using UnityEngine;

public class VehicleShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;  // Ateş noktasını araca ekleyip referans verebilirsin
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Sol tıklama
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Kurşunu ateşle
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed;  // Kurşunun ileri gitmesi
    }
}

