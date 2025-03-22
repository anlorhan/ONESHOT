using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeToDestroy = 2f;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);  // Efekt 2 saniye sonra yok olacak
    }
}
