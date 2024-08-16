using System.Collections;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public float damageAmount = 1f;
    public Animator animator;
    private Rigidbody2D rb;
    //public float groundOffset = 0.1f; // ���� ���� ������ ����

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // ���� �� �߷� ��Ȱ��ȭ
        rb.isKinematic = true;

        // 1�� �� �߷� Ȱ��ȭ
        StartCoroutine(ActivateGravityAfterDelay(0.5f));
    }

    private IEnumerator ActivateGravityAfterDelay(float delay)
    {
        // delay �� �߷�Ȱ��ȭ
        yield return new WaitForSeconds(delay);

        rb.isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamagable damagable = collision.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.TakeDamage(damageAmount);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("groundLayer"))
        {
            //Ʈ�� ������Ʈ�� ���� ���� �ణ �̵���Ŵ
            //transform.position = new Vector3(transform.position.x, collision.transform.position.y + groundOffset, transform.position.z);

            // ���鿡 ������ ������Ʈ�� �̵��� ���߰� �ִϸ��̼� ���
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            animator.Play("TreeObject_Drop");
            Destroy(gameObject, 0.5f);
        }
    }
}