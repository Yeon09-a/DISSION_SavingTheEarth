using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject[] field; 
    public Sprite[] crops; // 0 : �۹� ����, 1 : ���� ����, 2 : ���� ��, 3 : ���� �۹�, 4 : �丶�� ����, 5 : �丶�� ��, 6 : �丶�� �۹�
    public Sprite[] fieldSprite; // �ʵ� ��������Ʈ �迭(�ܵ�, �� ��) 0 : �ܵ�, 1 : �� ��

    public int[] fieldSpIndex; // 0 : �ܵ�, 1 : �� ��, 2 : ���� ��
    public int[] fieldCrops; // 0 : �۹� ����, 1 : ���� ����, 2 : ���� ��, 3 : ���� �۹�, 4 : �丶�� ����, 5 : �丶�� ��, 6 : �丶�� �۹�

    private GameObject playerObj;
    private Player player;
    private PlayerFarm playerFarm;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        playerFarm = playerObj.GetComponent<PlayerFarm>();
        player.ChangeCurTool -= ChangeCurTool;
        player.ChangeCurTool += ChangeCurTool;
        playerFarm.SetField -= SetField;
        playerFarm.SetField += SetField;

        fieldSpIndex = DataManager.instance.nowPlayerData.fieldSpIndex;
        fieldCrops = DataManager.instance.nowPlayerData.fieldCrops;

        for (int i = 0; i < 18; i++)
        {
            //SetField(i, fieldSpIndex[i], fieldCrops[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetField(int fieldNum, int spIndex, int cropIndex)
    {
        if (spIndex != 2)
        {
            field[fieldNum].GetComponent<SpriteRenderer>().sprite = fieldSprite[spIndex];
        } else
        {
            field[fieldNum].GetComponent<SpriteRenderer>().sprite = fieldSprite[1];
            field[fieldNum].GetComponent<SpriteRenderer>().color = new Color(198/255f, 198 / 255f, 198 / 255f);

        }

        fieldSpIndex[fieldNum] = spIndex;

        field[fieldNum].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (cropIndex != 0 ? crops[cropIndex] : null);
        fieldCrops[fieldNum] = cropIndex;
    }

    private void ChangeCurTool(int curTool)
    {
        playerFarm.curTool = curTool;
    }
}
