using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestWitchController : EnemyController
{
    public AttackSO appleAttack;
    public AttackSO thornAttack;
    public AttackSO dropAttack;

    private Animator animator;
    private Coroutine attackCoroutine;
    private Coroutine patternCoroutine;
    public GameObject player; // �÷��̾� ������Ʈ�� ������ ����
    
    private bool isPlayerInRange = false;
    public bool isMove = false;
    public bool isHit = false;
    public bool isDead = false;

    protected override void Start()
    {
        //SoundManager.Instance.PlayBGM("Mountain Town");

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // BossStatHandler ���
        gameObject.AddComponent<BossStatHandler>();

        // �÷��̾� ������Ʈ ã��
        player = GameObject.FindGameObjectWithTag("Player");

        // �÷��̾� ���� ����
        StartCoroutine(CheckForPlayer());

        // ���� �ڷ�ƾ ����
        StartCoroutine(AttackCycleRoutine());
    }

    private void FixedUpdate()
    {
        // �÷��̾ ���� �ۿ� ���� �� �÷��̾� ����
        if (!isAttack && !isPlayerInRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Vector2 targetPosition = rb.position + new Vector2(direction.x, direction.y) * enemyStats.moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // �̵� �ִϸ��̼� ���
            isMove = true;
            animator.SetBool("isMove", isMove);
        }
        else
        {
            // �̵� �ִϸ��̼� ����
            isMove = false;
            animator.SetBool("isMove", isMove);
        }
    }

    private IEnumerator CheckForPlayer()
    {
        while (enemyStats.maxHP > 0)
        {
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= detectionRange)
                {
                    isPlayerInRange = true;
                }
                else
                {
                    isPlayerInRange = false;
                }
            }

            yield return new WaitForSeconds(0.5f); // �÷��̾� ���� �ֱ�
        }
    }

    private IEnumerator AttackCycleRoutine()
    {
        while (enemyStats.maxHP > 0)
        {
            // appleAttack 3�� �ݺ� ���� (1.5�ʸ���)
            for (int i = 0; i < 3; i++)
            {
                ExecuteAttack(appleAttack);
                yield return new WaitForSeconds(3f);
            }

            // thornAttack �Ǵ� dropAttack�� �������� ����
            AttackSO selectedAttack = Random.value > 0.5f ? thornAttack : dropAttack;
            ExecuteAttack(selectedAttack);

            // ���� ����Ŭ �� ��� �ð�
            yield return new WaitForSeconds(4f); // 4�� ��� �� ���� ����Ŭ
        }
    }


    private void ExecuteAttack(AttackSO attack)
    {
        if (attack != null && enemyStats.maxHP > 0 && !isAttack)
        {
            // ���� �ִϸ��̼� Ʈ���� ���� �� ���� ����
            isAttack = true;
            switch (attack.name)
            {
                case "AppleAttack":
                    animator.SetTrigger("AppleAttack");
                    break;
                case "DropAttack":
                    animator.SetTrigger("DropAttack");
                    break;
                case "ThornAttack":
                    animator.SetTrigger("ThornAttack");
                    break;
            }
        }
    }

    public void OnHit()
    {
        animator.SetBool("isHit", isHit);
    }

    public override void OnDeath()
    {
        isDead = true;

        // ��� �ִϸ��̼� Ʈ���ſ� bool ���� �ʱ�ȭ�Ͽ� �ִϸ��̼��� ����
        animator.ResetTrigger("AppleAttack");
        animator.ResetTrigger("DropAttack");
        animator.ResetTrigger("ThornAttack");
        StopAllCoroutines();

        animator.SetBool("isMove", false);
        animator.SetBool("isHit", false);
        animator.SetBool("isDead", isDead);
    }

    public void OnDeathAnimationEnd()
    {
        Invoke("EndScene", 2f);
        Destroy(this.gameObject, 2f);
    }

    private void EndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void SpawnThornTraps()
    {
        if (thornAttack != null)
        {
            thornAttack.Attack(this.gameObject);
        }
        isAttack = false; // ������ ���� �� �̵� �簳
    }

    public void ThrowApple()
    {
        if (appleAttack != null)
        {
            appleAttack.Attack(this.gameObject);
        }
        isAttack = false;
    }

    public void DropTree()
    {
        if (dropAttack != null)
        {
            dropAttack.Attack(this.gameObject);
        }
        isAttack = false; // ������ ���� �� �̵� �簳
    }

    public void OnAnimationEnd()
    {
        isAttack = false;
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        if (patternCoroutine != null)
        {
            StopCoroutine(patternCoroutine);
            patternCoroutine = null;
        }
    }

    // ���� ������ ���� �׸��� ���� Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}