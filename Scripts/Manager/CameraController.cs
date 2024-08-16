using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        // Player �±׸� ���� ������Ʈ�� ã�� �Ҵ�
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            //Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        // ī�޶��� ��ġ�� �÷��̾��� ��ġ�� ����
        Vector3 newPosition = player.transform.position;
        newPosition.z = this.transform.position.z; // ī�޶��� z�� ��ġ�� �״�� ����
        this.transform.position = newPosition;
    }
}