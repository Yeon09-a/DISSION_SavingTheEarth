using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource1;
    public AudioSource musicsource2;
    //�ش������� ���� �ҷ����°� ��ȿ�����̶� ���� ����ÿ� ����Ʈ���·� �����Ͽ� ȣ��
    //public AudioSource musicsource3;
    public AudioSource btnsource;

    void Awake()
    {

        // ���� ���� ���� "BaseMap"�̸� musicsource1�� Ȱ��ȭ�ϰ�, "SeaMap"�̸� musicsource2�� Ȱ��ȭ�մϴ�.
        if (GameManager.instance.curMap == MapName.BaseMap)
        {
            musicsource1.gameObject.SetActive(true);
            musicsource2.gameObject.SetActive(false);
            //musicsource3.gameObject.SetActive(false);
        }
        else if (GameManager.instance.curMap == MapName.SeaMap)
        {
            musicsource1.gameObject.SetActive(false);
            musicsource2.gameObject.SetActive(true);
            //musicsource3.gameObject.SetActive(false);

            /*if(����������)
            {
                musicsource1.gameObject.SetActive(false);
                musicsource2.gameObject.SetActive(false);
                musicsource3.gameObject.SetActive(true);

            }*/
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicsource1.volume = volume;
        musicsource2.volume = volume;
        //musicsource3.volume = volume;
    }

    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
    }

    public void OnEffect()
    {
        btnsource.Play();
    }
}
