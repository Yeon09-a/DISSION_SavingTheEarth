using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmInfoPanel : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(OffFarmInfo());
    }

    IEnumerator OffFarmInfo()
    {
        Debug.Log("����");
        yield return new WaitForSecondsRealtime(5f);
        Debug.Log("����");
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(OffFarmInfo());
    }
}
