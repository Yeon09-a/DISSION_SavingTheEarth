using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public string itName; // ������ �̸�
    public int id; // ������ id(�迭 �ε����� ����)
    [TextArea]
    public string info; // ������ ����
    public Sprite image; // ������ �̹���
    public int type; // ������ ����(1 : ����ǰ, 2 : �߿买ǰ)
    public int price; // ������ ����
}
