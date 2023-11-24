using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaMap : MonoBehaviour
{
    public GameObject SeaMapFace;
    public SeaMapPanel SeaMapPanel;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ������ Player�� ã�ƺ�
        Player player = GameObject.FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("���� ������ Player�� ã�� �� �����ϴ�.");
        }
        else
        {
            Debug.Log("���� ������ Player�� ã�ҽ��ϴ�.");
        }

        // SeaMapPanel�� �̺�Ʈ �ڵ鷯���� ����
        SeaMapPanel.activateFace += ActivateFace;
        SeaMapPanel.deactivateFace += DeactivateFace;
    }

    // Update is called once per frame
    void Update()
    {
        // �Է� �Ǵ� ��Ÿ ������ Ȯ���� �� ����
    }

    private void ActivateFace()
    {
        SeaMapFace.SetActive(true);
    }

    private void DeactivateFace()
    {
        SeaMapFace.SetActive(false);
    }
}
