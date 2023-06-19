using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SetPlant : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject potatoPrefab;
    public float zOffsetFactor = 0.1f;
    [SerializeField] private SeedArrange SA;
    private int seedsCount;
    public Tile dirt;

    private HashSet<Vector3Int> potatoCells = new HashSet<Vector3Int>();

    private void Start()
    {
        seedsCount = SA.seedsCount;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем, нажата ли левая кнопка мыши
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем позицию мыши в мировых координатах

            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos); // Преобразуем позицию мыши в координаты тайла на Tilemap

            if (!potatoCells.Contains(cellPos))
            {
                if (potatoCells.Count == 0)
                {
                    // Размещаем первую картошку на любом тайле
                    PlacePotato(cellPos);
                    potatoCells.Add(cellPos);
                    SetTilesAround(cellPos);
                }
                else
                {
                    // Проверяем, соединен ли текущий тайл с другими тайлами с картошками
                    if (tilemap.GetTile(cellPos) == dirt)
                    {
                        // Размещаем картошку на соединенном тайле
                        PlacePotato(cellPos);
                        potatoCells.Add(cellPos);
                    }
                }
            }
        }
    }

    private void PlacePotato(Vector3Int cellPos)
    {
        // Получаем позицию центра тайла
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);

        // Получаем ряд тайла в иерархии
        int row = cellPos.y;

        // Вычисляем смещение по оси Z на основе позиции ряда
        float zOffset = row * zOffsetFactor - 50f;

        // Применяем смещение по оси Z к позиции картошки
        cellCenterPos.z += zOffset;

        // Создаем префаб картошки на позиции центра тайла
        if (SA.seedsCount > 0)
        {
            Instantiate(potatoPrefab, cellCenterPos, Quaternion.identity);
            SetTilesAround(cellPos);
            SA.Decrease();
        }
        
    }
    private void SetTilesAround(Vector3Int cellPos)
    {
        Vector3Int upTile = cellPos + Vector3Int.up;
        Vector3Int downTile = cellPos + Vector3Int.down;
        Vector3Int leftTile = cellPos + Vector3Int.left;
        Vector3Int rightTile = cellPos + Vector3Int.right;
        tilemap.SetTile(cellPos, dirt);
        tilemap.SetTile(upTile, dirt);
        tilemap.SetTile(downTile, dirt); 
        tilemap.SetTile(leftTile, dirt);
        tilemap.SetTile(rightTile, dirt);
    }
}
