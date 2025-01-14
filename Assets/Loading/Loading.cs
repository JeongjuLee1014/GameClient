using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class Loading : MonoBehaviour
{
    private string nextScene;
    [SerializeField] private Slider progressBar; // �ε� �� �����̴�

    private void Start()
    {
        StartCoroutine(InitializeAndLoad());
    }

    private IEnumerator InitializeAndLoad()
    {
        LoginValues loginValues = LoginValues.Get();

        if (loginValues == null || !loginValues.isLogined)
        {
            // ��α��� ����
            nextScene = "Login";
        }
        else
        {
            // �α��� ���� - �������� ����� ������ �ε�
            nextScene = "Home";
            yield return StartCoroutine(LoadUserData());
        }

        // �� �ε� ����
        yield return StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadUserData()
    {
        LoginValues loginValues = LoginValues.Get();
        string url = $"{Constant.SERVER_URL}/api/users/session/{loginValues.sessionId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                User loadedUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text);

                User.Instance.UpdateUserData(
                    //loadedUser.id,
                    loadedUser.nickName,
                    loadedUser.sessionId,
                    loadedUser.numCoins,
                    loadedUser.numStars,
                    loadedUser.numEnergies
                );

                Debug.Log("User data loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load user data: " + request.error);
                nextScene = "Login"; // ������ �ε� ���� �� �α��� ȭ������ �̵�
            }
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float displayProgress = 0.0f;

        while (!op.isDone)
        {
            // �ε� ����� ���
            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
            displayProgress = Mathf.Lerp(displayProgress, targetProgress, 0.01f);

            // �ε� �� ������Ʈ
            UpdateProgressBar(displayProgress);

            if (targetProgress >= 0.99f && displayProgress >= 0.99f)
            {
                op.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }

    private void UpdateProgressBar(float progress)
    {
        if (progressBar != null)
        {
            progressBar.value = progress;
        }
    }
}