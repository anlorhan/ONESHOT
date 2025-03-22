using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject cam;                          // Kamerayı tanımlıyoruz
    public GameObject backgroundPrefab;             // Arka plan prefab'i
    public float backgroundSize = 20.48f;           // Arka plan karelerinin boyutu (örneğin, 2048x2048 piksel için 20.48 birim)

    private Vector2Int camGridPosition;             // Kameranın hangi grid'de olduğunu belirlemek için
    private Dictionary<Vector2Int, GameObject> activeBackgrounds = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        // Başlangıç konumunda 3x3 bir arka plan grid'i oluştur
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                Vector3 worldPos = new Vector3(x * backgroundSize, y * backgroundSize, 0);
                GameObject bgTile = Instantiate(backgroundPrefab, worldPos, Quaternion.identity, transform);
                activeBackgrounds[gridPos] = bgTile;
            }
        }

        UpdateCameraGridPosition();
    }

    void Update()
    {
        // Kamera grid konumunu güncelle ve gerekirse arka planları güncelle
        Vector2Int newCamGridPos = GetCameraGridPosition();
        if (newCamGridPos != camGridPosition)
        {
            camGridPosition = newCamGridPos;
            UpdateBackgroundTiles();
        }
    }

    Vector2Int GetCameraGridPosition()
    {
        // Kameranın hangi grid'de olduğunu bulmak için konumunu backgroundSize ile böleriz
        int gridX = Mathf.RoundToInt(cam.transform.position.x / backgroundSize);
        int gridY = Mathf.RoundToInt(cam.transform.position.y / backgroundSize);
        return new Vector2Int(gridX, gridY);
    }

    void UpdateCameraGridPosition()
    {
        // Kamera pozisyonunu grid koordinatlarına dönüştür
        camGridPosition = GetCameraGridPosition();
    }

    void UpdateBackgroundTiles()
    {
        // Çevresinde bulunması gereken grid pozisyonları listesi
        List<Vector2Int> requiredGrids = new List<Vector2Int>();

        // 3x3 grid alanında her yöne kontrol ederek yeni arka plan parçaları oluştur
        for (int x = camGridPosition.x - 1; x <= camGridPosition.x + 1; x++)
        {
            for (int y = camGridPosition.y - 1; y <= camGridPosition.y + 1; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                requiredGrids.Add(gridPos);

                // Eğer bu grid pozisyonunda arka plan parçası yoksa bir tane oluştur
                if (!activeBackgrounds.ContainsKey(gridPos))
                {
                    Vector3 worldPos = new Vector3(x * backgroundSize, y * backgroundSize, 0);
                    GameObject bgTile = Instantiate(backgroundPrefab, worldPos, Quaternion.identity, transform);
                    activeBackgrounds[gridPos] = bgTile;
                }
            }
        }

        // Uzakta kalan arka plan parçalarını yok et
        List<Vector2Int> gridsToRemove = new List<Vector2Int>();

        foreach (var bgTile in activeBackgrounds)
        {
            if (!requiredGrids.Contains(bgTile.Key))
            {
                gridsToRemove.Add(bgTile.Key);
            }
        }

        // Dictionary'den ve sahneden sil
        foreach (var gridPos in gridsToRemove)
        {
            Destroy(activeBackgrounds[gridPos]);
            activeBackgrounds.Remove(gridPos);
        }
    }
}
