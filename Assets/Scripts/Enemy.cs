using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform worldCenter;  // Dünyanın merkezi
    public float speed = 2f;
    public int score=50;
    public int money=10;
    public int exp = 10;

    public GameObject explosionPrefab;  // Patlama efektinin prefab'ı
    public GameObject coinObject;

    void Update()
    {
        // Dünyanın merkezine doğru hareket et
        Vector3 direction = (worldCenter.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    // Düşman yok edilirse
    public void DestroyEnemy()
    {
        // Patlama efektini oynat
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Düşmanı yok et
        Destroy(gameObject);

        // Puan ve para ekle
        ScoreManager.instance.AddScore(score);
        ScoreManager.instance.AddMoney(money);

        GameObject coin=Instantiate(coinObject, transform.position, Quaternion.identity);
        coin.GetComponent<Coin>().exp = exp;
    }
}
