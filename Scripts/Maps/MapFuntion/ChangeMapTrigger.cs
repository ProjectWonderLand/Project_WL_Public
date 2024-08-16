using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMapTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        //Debug.Log("�� ��ȯ ����: " + nextSceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("�� �ε� �Ϸ�: " + scene.name);
        GameManager.Instance.FindAndAssignPlayer();
        GameManager.Instance.InitializeCurrentScene();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}