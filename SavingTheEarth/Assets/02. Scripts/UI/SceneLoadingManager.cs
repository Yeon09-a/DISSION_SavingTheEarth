using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public Image progressBar; // �ε� ��
    private static string nextScene; // �̵��� ���� ��
    private float fullSize = 718; // �ε� �� ���� ������
    private RectTransform progressBarRt; // �ε� �� RectTransform

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ�ȭ
        progressBarRt = progressBar.GetComponent<RectTransform>();
        
        // �ڷ�ƾ �ε� ����
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName) // �� �̵� �Լ�
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading"); // �ε��� �̵�
    }


    IEnumerator LoadScene() // �� �̵� �ڷ�ƾ
    {
        yield return null;

        // �񵿱� �� �̵�
        if (nextScene != "Title")
        {
            AsyncOperation op1 = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            AsyncOperation op2 = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

            // �� ��Ȱ��ȭ
            op1.allowSceneActivation = false;
            op2.allowSceneActivation = false;

            while (!op1.isDone && !op2.isDone)
            {
                yield return null;

                // �ε� �� ������Ʈ
                float op1Value = Mathf.Lerp(0, fullSize, op1.progress);
                float op2Value = Mathf.Lerp(0, fullSize, op2.progress);

                progressBarRt.sizeDelta = new Vector2(op1Value + op2Value, 40);

                if (op1.progress >= 0.9f && op2.progress >= 0.9f) // �ε� �Ϸ� ��
                {
                    progressBarRt.sizeDelta = new Vector2(fullSize * 2, 40);

                    yield return new WaitForSeconds(0.5f);

                    // �� Ȱ��ȭ
                    op1.allowSceneActivation = true;
                    op2.allowSceneActivation = true;
                }
            }
        } else
        {
            AsyncOperation op2 = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

            op2.allowSceneActivation = false;

            while (!op2.isDone)
            {
                yield return null;

                // �ε� �� ������Ʈ
                float op2Value = Mathf.Lerp(0, fullSize, op2.progress);

                progressBarRt.sizeDelta = new Vector2(op2Value, 40);

                if (op2.progress >= 0.9f) // �ε� �Ϸ� ��
                {
                    progressBarRt.sizeDelta = new Vector2(fullSize * 2, 40);

                    yield return new WaitForSeconds(0.5f);

                    // �� Ȱ��ȭ
                    op2.allowSceneActivation = true;
                }
            }
        }
        

        SceneManager.UnloadSceneAsync("Loading"); // �ε� �� ����
    }
}
