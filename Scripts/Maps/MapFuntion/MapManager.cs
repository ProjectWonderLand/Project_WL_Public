using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    public GameObject currentMap; // ���� �� ������
    public Transform mapParent; // ���� ��ġ�� �θ� ������Ʈ
    public Transform player; // �÷��̾��� Transform
    public Vector3 defaultPlayerPosition; // �÷��̾��� �⺻ ��ġ

    public void LoadMap(GameObject mapPrefab)
    {
        StartCoroutine(LoadMapAsync(mapPrefab));
    }

    private IEnumerator LoadMapAsync(GameObject mapPrefab)
    {
        // ���� �� ��ε�
        if (currentMap != null)
        {
            Destroy(currentMap);
            yield return Resources.UnloadUnusedAssets();
            System.GC.Collect(); // ������ �÷��� ���� ����
        }

        yield return null; // �� ������ ���

        // ���ο� �� �ε�
        currentMap = Instantiate(mapPrefab, mapParent);

        // �÷��̾� ��ġ ����
        Transform startPoint = currentMap.transform.Find("StartPosition");
        if (startPoint != null)
        {
            player.position = startPoint.position;
        }
        else
        {
            //Debug.LogWarning("StartPosition not found in the new map.");
            player.position = defaultPlayerPosition; // �⺻ ��ġ�� ����
        }
    }
}