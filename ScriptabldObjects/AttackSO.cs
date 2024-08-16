using UnityEngine;

public abstract class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public string animationTrigger; // �ִϸ��̼� Ʈ���� �̸� �߰� ( MeleeAttack or RangeAttack�� �ۼ�)
    public float attackDelay;
    public float attackPower;
    public float attackSpeed;
    public float attackRange;
    public LayerMask target;
    public AttackType attackType;

    // Ÿ�Ժ� ���ݷ��� �ۼ��� ��Ű���ϱ� ���� �߻�޼�����
    public abstract void Attack(GameObject attacker); // attacker�� ���� ��ġ�� ���� ������ �����´�
}