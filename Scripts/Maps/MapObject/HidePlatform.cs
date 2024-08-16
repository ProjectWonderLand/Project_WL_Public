using UnityEngine;
using UnityEngine.Tilemaps;

public class HidePlatform : MonoBehaviour
{
    public Tilemap tilemap; // Ÿ�ϸ� ������Ʈ�� ����
    public TileBase activeTile; // Ȱ��ȭ�� ������ Ÿ��
    public TileBase inactiveTile; // ��Ȱ��ȭ�� ������ Ÿ��
    public Vector3Int[] tilePositions; // Ÿ�� ��ġ �迭
    public string platformTag; // ������Ʈ Ǯ���� ����� �±�

    private int currentIndex = 0; // ���� Ȱ��ȭ�� Ÿ�� �ε���

    void Start()
    {
        // �ʱ�ȭ �� ù ��° Ÿ�ϸ� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ
        for (int i = 0; i < tilePositions.Length; i++)
        {
            tilemap.SetTile(tilePositions[i], inactiveTile);
        }
        if (tilePositions.Length > 0)
        {
            ActivateTile(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ���� Ÿ�� ��Ȱ��ȭ
            tilemap.SetTile(tilePositions[currentIndex], inactiveTile);

            // ���� Ÿ�� Ȱ��ȭ
            currentIndex = (currentIndex + 1) % tilePositions.Length;
            ActivateTile(currentIndex);
        }
    }

    void ActivateTile(int index)
    {
        tilemap.SetTile(tilePositions[index], activeTile);

        // ������Ʈ Ǯ���� ���� ������Ʈ�� �����ͼ� Ȱ��ȭ
       // GameObject platform = ObjectPool.Instance.SpawnFromPool(platformTag, tilemap.CellToWorld(tilePositions[index]) + tilemap.tileAnchor, Quaternion.identity);
        //platform.SetActive(true);
    }
}