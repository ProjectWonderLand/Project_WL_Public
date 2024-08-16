using UnityEngine;

public class SpriteChanger : InteractiveObject
{
    [SerializeField] private Sprite defaultSprite;  // �⺻ ��������Ʈ
    [SerializeField] private Sprite activeSprite;  // ��ȣ�ۿ� �� ��������Ʈ

    private SpriteRenderer spriteRenderer;
    private bool isActive = false;

    public bool IsActive => isActive;

    public event System.Action OnInteractionEvent;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = defaultSprite;  // �ʱ� ��������Ʈ ����
        }
    }

    protected override void OnInteraction()
    {
        if (spriteRenderer != null && !isActive)
        {
            isActive = true;
            spriteRenderer.sprite = activeSprite;  // ��������Ʈ ����

            OnInteractionEvent?.Invoke();  // �̺�Ʈ ȣ��
            //Debug.Log("SpriteChanger interacted with and event invoked.");
        }
    }
}