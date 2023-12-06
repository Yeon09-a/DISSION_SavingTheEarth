using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarm : MonoBehaviour
{
    private RaycastHit2D hit; // ����ĳ��Ʈ �ᱣ���� �����ϱ� ���� ����ü ����

    GameObject scanObject; // ���̿� �浹�� ������Ʈ ����

    [SerializeField]
    private int fieldNum; // ���õ� fieldNum
    [SerializeField]
    private int curState; // 0 : �ܵ�, 1 : �� ��, 2 : ���� ��
    [SerializeField]
    private int cropState; // 0 : ����, 1 : ���� ����, 2 : ���� ��, 3 : ���� �۹�, 4 : �丶�� ����, 5 : �丶�� ��, 6 : �丶�� �۹�

    public int curTool = 0; // ���� �÷��̾ ������ ���� 0 : ����, 1 : ȣ��, 2 : ���Ѹ���, 3 : ���� �ٱ���
    public int seedCount = 0;

    public Action<int, int, int> SetField;
    public Action<int> SetSeedCount;
    public Action<string> OnFarmInfo;

    public InventoryManager invenMng;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ��ȣ�ۿ� Ű
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector3.forward * -1, 1.0f, 1 << 9); // ���� �߻�
            if (hit.collider != null) // �浹�� ������Ʈ�� ���� ���
            {
                // ���⿡�� ��ȣ�ۿ�
                // hit.collider�� ���̿� �浹�� ������Ʈ
                scanObject = hit.collider.gameObject;

                Debug.Log(scanObject.name);

                if (scanObject.CompareTag("FarmField"))
                {
                    Vector3 fieldInfo = scanObject.GetComponent<Field>().fieldInfo;
                    fieldNum = (int)fieldInfo.x;
                    curState = (int)fieldInfo.y;
                    cropState = (int)fieldInfo.z;

                    if (cropState != 3 && cropState != 6)
                    {
                        CheckField();
                    }
                    else
                    {
                        bool isNotFull = invenMng.PutItem(cropState == 3 ? 6 : 7, 1, 0);

                        if (!isNotFull)
                        {
                            // ���ڿ� ��.
                            OnFarmInfo("���濡 �� �ڸ��� ���� ��Ȯ��\n���ڸ� ���ڿ� �־����ϴ�.");

                            // ����
                        }
                    }
                }
            }
            else
                scanObject = null;
        }
    }

    private void CheckField()
    {
        switch (curTool)
        {
            case 1: // ȣ��
                if (curState == 0 && cropState == 0) // �ܵ�
                {
                    scanObject.GetComponent<Field>().fieldInfo = new Vector3(fieldNum, 1, 0);
                    SetField(fieldNum, 1, 0);
                }
                break;
            case 2: // ���Ѹ���
                if (curState == 1) // �� ��
                {
                    scanObject.GetComponent<Field>().fieldInfo = new Vector3(fieldNum, 2, cropState);
                    SetField(fieldNum, 2, cropState);
                }
                break;
            case 3: // ���� �ٱ���
                if (curState != 0 && cropState == 0)
                {
                    int count = DataManager.instance.nowPlayerData.haveItems.ContainsKey(6) ? DataManager.instance.nowPlayerData.haveItems[6].count : 0;

                    if (count > 0)
                    {
                        count--;
                        SetSeedCount(count);
                        invenMng.UseItem(6, 1);
                        scanObject.GetComponent<Field>().fieldInfo = new Vector3(fieldNum, curState, 1);
                        SetField(fieldNum, curState, 1);
                    }
                    else
                    {
                        OnFarmInfo("���� ���� ������ �����ϴ�.");
                    }

                }
                break;
        }
    }
}
