using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButtonManager : MonoBehaviour
{
    public GameObject settingPanel;

    private void Start()
    {
        // StartScene BGM
        SoundManager.Instance.PlayBGM("WayBack");
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
        }
    }

    public void NewGameBtn() // Ʃ�丮��
    {
        //Debug.Log("���� ����");
        SoundManager.Instance.PlaySFX("Button_2");
        SoundManager.Instance.PlayBGM("SpringTime");
        SceneManager.LoadScene("TutorialScene"); // ���̸� ����
    }

    public void LoadGameBtn()
    {
        //Debug.Log("���� �ҷ�����");
        SoundManager.Instance.PlaySFX("Button_2");
    }

    public void SettingBtn()
    {
        //Debug.Log("���� ����");
        SoundManager.Instance.PlaySFX("Box_Open_2");
        settingPanel.SetActive(true);
    }

    public void BackBtn()
    {
        SoundManager.Instance.PlaySFX("Box_Close_2");
        settingPanel.SetActive(false);
    }

    public void ExitBtn()
    {
        //Debug.Log("Exit");
        SoundManager.Instance.PlaySFX("Box_Close_2");
        Application.Quit();
    }
}