using UnityEngine;

public class OrbitingVehicle : MonoBehaviour
{
    public Transform worldCenter;  // Dünyanın ortasındaki daire (referansı Unity'den sürükleyip bırak)
    public float rotationSpeed = 100f;
    public float distanceToEarth;
    public bool isPlayer;

    void Update()
    {
        // Fare pozisyonunu dünya etrafında al
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Araç dünya etrafında dönecek
        Vector3 direction = (mousePosition - worldCenter.position).normalized;
        transform.position = worldCenter.position + direction * distanceToEarth;

        // Araç yüzü mouse'a bakacak
        transform.up = direction;
    }
}

