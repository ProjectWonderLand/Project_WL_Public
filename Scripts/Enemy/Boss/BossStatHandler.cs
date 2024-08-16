using UnityEngine;
using System.Collections;

public class BossStatHandler : MonoBehaviour, IDamagable
{
    private ForestWitchController forestWitchController;
    private EnemyStats enemyStats;
    private SpriteRenderer spriteRenderer;

    private bool isInvincible = false; // ���� ���¸� ��Ÿ���� ����

    private void Start()
    {
        forestWitchController = GetComponent<ForestWitchController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyStats = forestWitchController.enemyStats;
    }

    private void Update()
    {
        if (enemyStats.maxHP <= 0)
        {
            forestWitchController.OnDeath();
        }
    }

    public void TakeDamage(float amount)
    {
        // return �Ͽ� �����ð�
        if (isInvincible)
        {
            return;
        }

        // ���� ���°� �ƴ� ���
        enemyStats.maxHP -= amount;

        // �ǰ� �� ���� ���·� ����
        StartCoroutine(BecomeInvincible());
    }

    private IEnumerator BecomeInvincible()
    {
        if (enemyStats.maxHP <= 0) yield break;

        isInvincible = true;
        forestWitchController.isHit = true;
        forestWitchController.OnHit();

        // ���� ���� ���� �ð� (3��)
        yield return new WaitForSeconds(3f);

        isInvincible = false;
        forestWitchController.isHit = false;
        forestWitchController.OnHit();

        yield break;
    }
}