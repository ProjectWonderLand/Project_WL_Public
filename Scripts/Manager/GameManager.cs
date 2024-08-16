using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public string playerName = "Player"; // ������ �÷��̾� ������Ʈ�� �̸�
    public GameObject playerPrefab; // �÷��̾� ������ ����
    public GameObject gameOverUIPrefab; // ���� ���� UI ������
    private GameObject player; // ���� Ȱ��ȭ�� �÷��̾� �ν��Ͻ�
    private GameObject gameOverUI; // ���� Ȱ��ȭ�� ���� ���� UI �ν��Ͻ�
    private bool isSceneInitialized = false; // �� �ʱ�ȭ�� �Ǿ����� Ȯ���ϴ� �÷���

    private PlayerStatHandler playerStatHandler;

    protected override void Awake()
    {
        base.Awake();
        //Debug.Log("GameManager Awake");

        // ���� ���� UI �������� �ν��Ͻ�ȭ�ϰ� ��Ȱ��ȭ
        if (gameOverUIPrefab != null && gameOverUI == null)
        {
            gameOverUI = Instantiate(gameOverUIPrefab);
            gameOverUI.SetActive(false);
            DontDestroyOnLoad(gameOverUI);
        }
    }

    private void Start()
    {
        //Debug.Log("GameManager Start");
        FindAndAssignPlayer();
    }

    public void FindAndAssignPlayer()
    {
        // �̹� �÷��̾� ������Ʈ�� �����ϴ��� Ȯ��
        player = GameObject.FindWithTag(playerName);

        if (player == null && playerPrefab != null)
        {
            //Debug.LogWarning("Player object not found, creating new one from prefab.");
            player = Instantiate(playerPrefab);
            player.name = playerName; // �÷��̾� �̸� ����
            DontDestroyOnLoad(player);
        }
        else if (player != null)
        {
            //Debug.Log("Player already assigned: " + player.name);
        }
        else
        {
            //Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� ������, �����յ� �������� �ʾҽ��ϴ�.");
            return;
        }

        playerStatHandler = player.GetComponent<PlayerStatHandler>();
        if (playerStatHandler != null)
        {
            playerStatHandler.FindHeartImages();  // ��Ʈ �̹����� �ٽ� ����
            playerStatHandler.InitializeHealthUI();  // �� �ʱ�ȭ �� ü�� UI�� �ʱ�ȭ
        }
    }

    // �� �ʱ�ȭ �޼���
    public void InitializeCurrentScene()
    {
        //Debug.Log("InitializeCurrentScene ȣ���");
        if (!isSceneInitialized)
        {
            isSceneInitialized = true; // �÷��׸� ���� ����
            StartCoroutine(InitializeCurrentSceneRoutine());
        }
    }

    private IEnumerator InitializeCurrentSceneRoutine()
    {
        yield return new WaitForEndOfFrame(); // �� �ε� �� �� ������ ���
        //Debug.Log("InitializeCurrentSceneRoutine ����");

        MapSettings mapSettings = FindObjectOfType<MapSettings>();
        if (mapSettings != null)
        {
            //Debug.Log("MapSettings�� ���������� ã�ҽ��ϴ�: " + mapSettings.gameObject.name);
            InitializeMapSettings(mapSettings);
        }
        else
        {
            //Debug.LogWarning("���� ������ MapSettings�� ã�� �� �����ϴ�.");
        }
    }

    private void InitializeMapSettings(MapSettings mapSettings)
    {
        // �÷��̾ StartPoint�� �̵�
        if (mapSettings.StartPoint != null && player != null)
        {
            //Debug.Log("�÷��̾ StartPoint�� �̵��մϴ�: " + mapSettings.StartPoint.position);
            player.transform.position = mapSettings.StartPoint.position;

            // MapDoorManager �ʱ�ȭ
            MapDoorManager doorManager = mapSettings.DoorPrefab.GetComponent<MapDoorManager>();
            if (doorManager != null)
            {
                doorManager.Initialize(mapSettings);
            }
            else
            {
                //Debug.LogWarning("MapDoorManager�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            //Debug.LogWarning("StartPoint�� �������� �ʾҰų� Player�� null�Դϴ�.");
        }
    }

    public void GameOver()
    {
        if (player != null)
        {
            player.GetComponent<PlayerStatHandler>().enabled = false;
        }
        Time.timeScale = 0;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameRoutine());
    }

    private IEnumerator RestartGameRoutine()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // ���� �÷��̾� ������Ʈ ����
        if (player != null)
        {
            Destroy(player);
            player = null;
        }

        // ��Ʈ UI ������Ʈ�� �ʱ�ȭ
        if (playerStatHandler != null)
        {
            playerStatHandler.heartImages.Clear();  // ����Ʈ �ʱ�ȭ
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isSceneInitialized = false; // ���� ����� �� �ʱ�ȭ �÷��׸� �ʱ�ȭ
        yield return null;

        FindAndAssignPlayer();

        if (playerStatHandler != null)
        {
            playerStatHandler.InitializeHealthUI();  // ü�� UI�� �ٽ� �ʱ�ȭ
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("�� �ε� �Ϸ� �� �ʱ�ȭ ����: " + scene.name);

        // ������ �÷��̾� ������Ʈ�� �����ϴ��� Ȯ���ϰ� �ʱ�ȭ
        if (player == null)
        {
            FindAndAssignPlayer();
        }

        isSceneInitialized = false; // �� �ε� �� �ʱ�ȭ �÷��׸� �ʱ�ȭ
        InitializeCurrentScene(); // ���� �ε�� �� �ٽ� �ʱ�ȭ ��ƾ�� ����

        if (playerStatHandler != null)
        {
            playerStatHandler.FindHeartImages();
            playerStatHandler.UpdateHealthUI(playerStatHandler.playerstats.playerCurHP);
        }
    }
}