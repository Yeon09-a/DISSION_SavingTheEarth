using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public string itName; // ������ �̸�
    public int id; // ������ id(�迭 �ε����� ����)
    public string info; // ������ ����
    public Texture image; // ������ �̹���
    public int type; // ������ ����
}
