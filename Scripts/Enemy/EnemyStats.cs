using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public string EnemyName;
    [Range(0.0f, 20.0f)] public float maxHP;
    public float moveSpeed;
    public float knockbackDistance = 1f;
    public AttackSO[] enemyAttackSO; // �ٰŸ��� ���Ÿ������� ����ϴ� ��츦 ����� �迭�� ���� << Enum���� Ÿ���� ���� (Basic, Both) ���� �� ������ �� �ֵ���
}