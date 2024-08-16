using UnityEngine;

[CreateAssetMenu(fileName = "DropTreehAttackSO", menuName = "AttackSO/Attacks/Stage1Boss/DropTreehAttackSO", order = 2)]
public class DropTreeAttackSO : MeleeAttackSO
{
    public GameObject treePrefab;
    public float offset = 3f; // �翷���� ������ �Ÿ�
    public int numberOfTrees = 10; // ������ ������ ��
    public float patternOffset = 0.5f; // �� ���� ������ ���� X ������

    public override void Attack(GameObject attacker)
    {
        //Debug.Log("DropTreeAttackSO Attack method called");
        Vector3 basePosition = attacker.transform.position;

        bool isFirstPattern = Random.value > 0.5f; // ������ �����ϰ� ����

        for (int i = 0; i < numberOfTrees; i++)
        {
            float xPosition = basePosition.x + (i - numberOfTrees / 2) * offset;

            if (isFirstPattern)
            {
                // ù��° ����: x������ -0.5
                Vector3 spawnPosition = new Vector3(xPosition - patternOffset, basePosition.y +2, basePosition.z);
                GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                // �ι�° ����: x������ +0.5
                Vector3 spawnPosition = new Vector3(xPosition + patternOffset, basePosition.y +2, basePosition.z);
                GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}