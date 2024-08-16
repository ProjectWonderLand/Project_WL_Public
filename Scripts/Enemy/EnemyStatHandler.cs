using UnityEngine;

public class EnemyStatHandler : MonoBehaviour, IDamagable
{
    private EnemyController enemyController;
    private EnemyStats enemyStats;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyStats = enemyController.enemyStats;
    }

    public void TakeDamage(float amount)
    {
        // �ǰ� ��� �߿��� return �Ͽ� �����ð�
        if (enemyController.enemyAnimationController.isHit == true)
        {
            return;
        }

        // �����ð��� �ƴ� ���
        enemyStats.maxHP -= amount;
        enemyController.OnHit();

        // �ڷ� �з����� ���� �߰�
        if (enemyController.ClosestTarget != null)
        {
            // �÷��̾���� ���� ���
            Vector2 knockbackDirection = (transform.position - enemyController.ClosestTarget.position).normalized;

            // ���� �ӵ��� ������� �ڷ� �з����� ���� ���ϱ� ���� �ӵ� �ʱ�ȭ
            enemyController.rb.velocity = Vector2.zero;

            // ���޽��� ���Ͽ� �з����� ��
            enemyController.rb.AddForce(enemyStats.knockbackDistance * knockbackDirection, ForceMode2D.Impulse);
        }

        // ���� ���� �� ������ ���
        if (enemyController.isAttack)
        {
            CancelAttack();
        }


        if (enemyStats.maxHP <= 0)
        {
            enemyController.OnDeath();
        }
    }

    private void CancelAttack()
    {
        enemyController.isAttack = false;
        enemyController.enemyAnimationController.ResetAttackAnimation();
    }
}