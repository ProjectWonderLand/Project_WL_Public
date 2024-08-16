using System;
using System.Collections;
using UnityEngine;

// Enemy�� �⺻������ �ش� ��ũ��Ʈ�� ��ӹ޴´�.

public class EnemyController : MonoBehaviour
{
    public Action<Vector2> OnMoveEvent;
    public Action<AttackSO> OnAttackEvent;
    public Action<EnemyController> OnDeathEvent;
    public Action<EnemyController> OnHitEvent;

    public Transform ClosestTarget { get; private set; }

    public EnemyAnimationController enemyAnimationController;
    public EnemyStats enemyStats; // EnemyStats�� ���� ���� �迭�� ������

    public LayerMask playerLayer;
    public float detectionRange = 10f; // �⺻ �÷��̾� ���� ����
    public float detectingTime = 0.5f; // �⺻ �÷��̾� ���� �ֱ�
    public Coroutine checkForPlayerCoroutine; // �÷��̾� üũ �ڷ�ƾ

    private EnemyMeleeAttack enemyMeleeAttack;
    private EnemyRangeAttack enemyRangeAttack;

    public bool attacked = false; // ������ �� ������ ����� ���� bool��
    public bool isAttack = false; // ���� ������ �Ǵ��ϴ� bool��

    public Rigidbody2D rb;

    protected virtual void Start()
    {
        checkForPlayerCoroutine = StartCoroutine(CheckForPlayer());

        enemyAnimationController = GetComponent<EnemyAnimationController>();
        enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        enemyRangeAttack = GetComponent<EnemyRangeAttack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator CheckForPlayer()
    {
        while (enemyStats.maxHP > 0)
        {
            // OverlapCircleAll : ���� ���� ���� �ִ� ��� Collider2D �˻��ϰ� �迭�� ��ȯ
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);

            if (hits.Length > 0) // playerLayer�� �ϳ��� hit�Ǹ�
            {
                ClosestTarget = hits[0].transform;

                // �÷��̾ �����Ǹ� ���� Ÿ�Կ� ���� ���� ����
                if (enemyMeleeAttack != null)
                {
                    enemyMeleeAttack.MeleeAttack();
                    attacked = true;
                }

                if (enemyRangeAttack != null)
                {
                    enemyRangeAttack.RangeAttack();
                    attacked = true;
                }
            }
            else
            {
                SetClosestTarget(null);
            }

            if (attacked)
            {
                // ���� Ÿ�Ժ��� ���� �� attackDelay�� ��ٸ�
                float maxDelay = 0f;
                foreach (var attackSO in enemyStats.enemyAttackSO)
                {
                    if (attackSO.attackDelay > maxDelay)
                    {
                        maxDelay = attackSO.attackDelay;
                    }
                }
                yield return new WaitForSeconds(maxDelay);
                isAttack = false;
                attacked = false;
            }
            else
            {
                yield return new WaitForSeconds(detectingTime);
            }
        }
    }

    // ClosestTarget�� private set�̹Ƿ� �ܺο��� ���� �ٲٷ��� �ش� �Լ� �̿�
    public void SetClosestTarget(Transform target)
    {
        ClosestTarget = target;
    }

    public void OnMove(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void OnAttack(AttackSO attack)
    {
        OnAttackEvent?.Invoke(attack);
    }

    public void OnHit()
    {
        OnHitEvent?.Invoke(this);
    }

    public virtual void OnDeath()
    {
        if (checkForPlayerCoroutine != null)
        {
            StopCoroutine(checkForPlayerCoroutine);
        }

        OnDeathEvent?.Invoke(this);
    }
}