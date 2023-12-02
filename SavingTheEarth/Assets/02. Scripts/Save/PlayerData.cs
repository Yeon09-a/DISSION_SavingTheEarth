using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerData // �÷��̾� ������ Ŭ����
{
    // �÷��̾� ���� ����
    public string playerName; // �÷��̾� �̸�
    public int playTime; // �÷��� Ÿ��(��)
    public string placeName; // ��� �̸�
    public int placeNum; // ��� ��ȣ
    public Vector3 playerPos; // �÷��̾� ��ǥ
    public int gameTime; // ���� �ð�
    public string ampm; // ����, ����
    public int money; // ��
    public int[] fieldSpIndex; // 0 : �ܵ�, 1 : �� ��, 2 : ���� ��
    public int[] fieldCrops; // 0 : �۹� ����, 1 : ����, 2 : �丶��

    // ������ ���� ����
    [NonSerialized]
    public Dictionary<int, HaveItemInfo> haveItems; // ���� ������ �ִ� ������ ��ųʸ�<������ id, ������ ����>

    public PlayerData(int playTime, string placeName, int placeNum, int gameTime, string ampm , Dictionary<int, HaveItemInfo> haveItems, int money) // �Ŀ� �÷��̾� ���� �߰��� �Ű����� �߰��� ����
    {
        this.playTime = playTime;
        this.placeName = placeName;
        this.placeNum = placeNum;
        this.gameTime = gameTime;
        this.ampm = ampm;
        this.haveItems = haveItems;
        this.money = money;
        fieldSpIndex = Enumerable.Repeat(0, 18).ToArray();
        fieldCrops = Enumerable.Repeat(0, 18).ToArray();

    }
}
