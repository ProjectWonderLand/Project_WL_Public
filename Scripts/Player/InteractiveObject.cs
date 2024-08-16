using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private PlayerController player;
    public ObjectType objectType;
    protected bool isInteractiveObject;

    protected void OnTriggerEnter2D(Collider2D collision) //������ ����
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            isInteractiveObject = true;

            if (player != null)
            {
                player.interactiveObject = this;

                if (objectType == ObjectType.Arrow)
                {
                    player.AddArrows(1);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInteractiveObject = false;

            if (player != null)
            {
                player.interactiveObject = null;
                player = null;
            }
        }
    }

    public void Interaction()
    {
        if (isInteractiveObject)
        {
            //Debug.Log("��ȣ�ۿ�");
            OnInteraction();
        }
    }

    protected virtual void OnInteraction()
    {
        // ��ӵ� Ŭ�������� ������ ����
    }
}