using System.Collections.Generic;
using UnityEngine;

public class MapSettings : MonoBehaviour
{
    [SerializeField] private int requiredActivatedStones;  // �ʿ��� Ȱ��ȭ�� ���� ����
    [SerializeField] private GameObject doorPrefab;  // �ʿ� ���� �� ������
    [SerializeField] private List<SpriteChanger> interactiveStones;  // �ʿ� �ִ� ���ͷ�Ƽ�� ���� ���
    [SerializeField] private Transform startPoint;  // �÷��̾��� ���� ����

    public int RequiredActivatedStones => requiredActivatedStones;
    public GameObject DoorPrefab => doorPrefab;
    public List<SpriteChanger> InteractiveStones => interactiveStones;
    public Transform StartPoint => startPoint;
}