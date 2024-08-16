using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint; // ���� ��ġ
    public Transform endPoint; // �� ��ġ
    public Transform alternateEndPoint; // ��ü �� ��ġ
    public float speed = 2f; // �̵� �ӵ�
    private bool movingToEnd = true;
    private bool usingAlternateEndPoint = false;
    private bool toggleRequested = false;

    void Update()
    {
        Transform targetPoint = usingAlternateEndPoint ? alternateEndPoint : endPoint;

        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint.position) < 0.01f)
            {
                movingToEnd = true;
                if (toggleRequested)
                {
                    ToggleEndPoint();
                    toggleRequested = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Debug.Log("Player entered platform.");
            collision.transform.SetParent(transform); // �÷��̾ �÷����� �ڽ����� ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Debug.Log("Player exited platform.");
            collision.transform.SetParent(null); // �÷��̾ �÷����� ����� �ڽ� ���� ����
            DontDestroyOnLoad(collision.gameObject); // �÷��̾ �ٽ� DontDestroyOnLoad ���·� ����
        }
    }

    public void RequestToggleEndPoint()
    {
        toggleRequested = true;
    }

    public void ToggleEndPoint()
    {
        usingAlternateEndPoint = !usingAlternateEndPoint;
    }
}