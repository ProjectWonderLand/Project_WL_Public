using UnityEngine;

[CreateAssetMenu(fileName = "ThornTrapAttackSO", menuName = "AttackSO/Attacks/Stage1Boss/ThornTrapAttackSO", order = 0)]
public class ThornTrapAttackSO : MeleeAttackSO
{
    public GameObject leftThornTrapPrefab;
    public GameObject rightThornTrapPrefab;

    public override void Attack(GameObject attacker)
    {
        Vector3 position = attacker.transform.position;

        // ���ø� ��ȯ�� ��ġ�� ����
        Vector3 leftPosition = new Vector3(position.x, position.y -3.8f, position.z);
        Vector3 rightPosition = new Vector3(position.x, position.y -3.8f, position.z);

       // ���� ������Ʈ�� ��ȯ
        if (leftThornTrapPrefab != null && rightThornTrapPrefab != null)
        {
            GameObject leftThorn = Instantiate(leftThornTrapPrefab, leftPosition, Quaternion.identity);
            GameObject rightThorn = Instantiate(rightThornTrapPrefab, rightPosition, Quaternion.identity);

            // 5�� �� ���� ������Ʈ ����
            Destroy(leftThorn, 3f);
            Destroy(rightThorn, 3f);
        }
    }
}