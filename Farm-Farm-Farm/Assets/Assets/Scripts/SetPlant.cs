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
        if (Input.GetMouseButtonDown(0)) // ���������, ������ �� ����� ������ ����
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // �������� ������� ���� � ������� �����������

            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos); // ����������� ������� ���� � ���������� ����� �� Tilemap

            if (!potatoCells.Contains(cellPos))
            {
                if (potatoCells.Count == 0)
                {
                    // ��������� ������ �������� �� ����� �����
                    PlacePotato(cellPos);
                    potatoCells.Add(cellPos);
                    SetTilesAround(cellPos);
                }
                else
                {
                    // ���������, �������� �� ������� ���� � ������� ������� � ����������
                    if (tilemap.GetTile(cellPos) == dirt)
                    {
                        // ��������� �������� �� ����������� �����
                        PlacePotato(cellPos);
                        potatoCells.Add(cellPos);
                    }
                }
            }
        }
    }

    private void PlacePotato(Vector3Int cellPos)
    {
        // �������� ������� ������ �����
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);

        // �������� ��� ����� � ��������
        int row = cellPos.y;

        // ��������� �������� �� ��� Z �� ������ ������� ����
        float zOffset = row * zOffsetFactor - 50f;

        // ��������� �������� �� ��� Z � ������� ��������
        cellCenterPos.z += zOffset;

        // ������� ������ �������� �� ������� ������ �����
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
