using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSeaMap : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        // �÷��̾ "OutDoor"�� �浹�ϰ� "E" Ű�� ������ "SeaMap"���� �̵�
        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.curMap = MapName.SeaMap;
            GameManager.instance.preMap = MapName.BaseMap;

            if (player.ScanObject != null && player.ScanObject.CompareTag("Door"))
            {
                SceneManager.LoadScene("SeaMap");
                SceneManager.LoadScene("Player", LoadSceneMode.Additive);
            }
        }
    }
}