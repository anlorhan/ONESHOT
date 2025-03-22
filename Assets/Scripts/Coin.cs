using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isInRange=false;
    public int exp;
    public float speed = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collective"))
        {
            isInRange = true;
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            isInRange = false;
        }

        ScoreManager.instance.AddExp(exp);
    }

    private void GoToWorld()
    {
        Vector3 direction = (ScoreManager.instance.worldCenter.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Update()
    {
        if (isInRange) {
            GoToWorld();
        }
    }
}
