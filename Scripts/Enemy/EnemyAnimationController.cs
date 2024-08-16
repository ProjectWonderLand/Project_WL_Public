using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;

    public bool isHit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();

        // �ִϸ��̼� �̺�Ʈ ���
        enemyController.OnMoveEvent += MoveAnimation;
        enemyController.OnAttackEvent += AttackAnimation;
        enemyController.OnHitEvent += HitAnimation;
        enemyController.OnDeathEvent += DeathAnimation;
    }

    private void MoveAnimation(Vector2 direction)
    {
        bool isMove = direction.x != 0; // direction.x�� 0�̾ƴϸ� true
        animator.SetBool("isMove", isMove);
    }

    private void AttackAnimation(AttackSO attack)
    {
        animator.SetTrigger(attack.animationTrigger);
    }

    private void HitAnimation(EnemyController enemyController)
    {
        isHit = true;
        animator.SetBool("isHit", isHit);
    }

    private void DeathAnimation(EnemyController controller)
    {
        animator.SetBool("isDead", true);
    }

    // ���ϸ��̼� �̺�Ʈ�� ȣ��� �޼���
    private void EndHitAnimation()
    {
        isHit = false;
        animator.SetBool("isHit", isHit); // Hit�� false�̹Ƿ� �ִϸ��̼� ����
    }

    // ���ϸ��̼� �̺�Ʈ�� ȣ��� �޼���
    private void TriggerMeleeAttack()
    {
        MeleeAttackSO meleeAttack = (MeleeAttackSO)enemyController.enemyStats.enemyAttackSO[0];
        meleeAttack.TriggerAttack(gameObject);
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(enemyController.gameObject);
    }

    public void ResetAttackAnimation()
    {
        animator.ResetTrigger("MeleeAttack");
        animator.ResetTrigger("RangeAttack");
        animator.SetBool("isAttack", false);
    }
}