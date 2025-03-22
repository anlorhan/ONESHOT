using System;
using UnityEngine;

public class CircleWorld : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private GameObject leftEngine;
    [SerializeField] private GameObject rightEngine;
    [SerializeField] private GameObject topEngine;
    [SerializeField] private GameObject bottomEngine;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        horizontalInput = inputHandler.MoveInput.x;
        verticalInput = inputHandler.MoveInput.y;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        float speed = moveSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        rb.linearVelocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        // Motorları sıfırla
        ResetEngines();

        // rb.velocity değerine göre motorları aktif et
        if (rb.linearVelocity.x > 0 && rb.linearVelocity.y > 0)
        {
            leftEngine.SetActive(true);
            bottomEngine.SetActive(true);
        }
        else if (rb.linearVelocity.x > 0 && rb.linearVelocity.y < 0)
        {
            leftEngine.SetActive(true);
            topEngine.SetActive(true);
        }
        else if (rb.linearVelocity.x < 0 && rb.linearVelocity.y > 0)
        {
            rightEngine.SetActive(true);
            bottomEngine.SetActive(true);
        }
        else if (rb.linearVelocity.x < 0 && rb.linearVelocity.y < 0)
        {
            rightEngine.SetActive(true);
            topEngine.SetActive(true);
        }
        else if (rb.linearVelocity.x > 0)
        {
            leftEngine.SetActive(true);
        }
        else if (rb.linearVelocity.x < 0)
        {
            rightEngine.SetActive(true);
        }
        else if (rb.linearVelocity.y > 0)
        {
            bottomEngine.SetActive(true);
        }
        else if (rb.linearVelocity.y < 0)
        {
            topEngine.SetActive(true);
        }
    }

    private void ResetEngines()
    {
        // Tüm motorları kapat
        leftEngine.SetActive(false);
        rightEngine.SetActive(false);
        topEngine.SetActive(false);
        bottomEngine.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameOver();
            Destroy(other.gameObject);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! World destroyed.");
        // Burada oyunu durdurma veya yeniden başlatma işlemlerini ekleyebilirsin.
    }
}
