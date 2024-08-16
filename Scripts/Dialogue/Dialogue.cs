using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int id;
    public int chapter;

    [Tooltip("��� ĳ����")]
    public string name;

    [Tooltip("��� ����")]
    public string[] contexts;

    [Tooltip("�̺�Ʈ")]
    public string eventFilter;

    [Tooltip("��ġ")]
    public string location;

    //[Tooltip("�ʻ�ȭ")]
    //public string portrait;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;
}