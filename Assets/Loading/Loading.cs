using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class Loading : MonoBehaviour
{
    private string nextScene;
    [SerializeField] private Slider progressBar; // 로딩 바 슬라이더

    private void Start()
    {
        StartCoroutine(InitializeAndLoad());
    }

    private IEnumerator InitializeAndLoad()
    {
        LoginValues loginValues = LoginValues.Get();

        if (loginValues == null || !loginValues.isLogined)
        {
            // 비로그인 상태
            nextScene = "Login";
        }
        else
        {
            // 로그인 상태 - 서버에서 사용자 데이터 로드
            nextScene = "Home";
            yield return StartCoroutine(LoadUserData());
        }

        // 씬 로딩 진행
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
                nextScene = "Login"; // 데이터 로드 실패 시 로그인 화면으로 이동
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
            // 로딩 진행률 계산
            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
            displayProgress = Mathf.Lerp(displayProgress, targetProgress, 0.01f);

            // 로딩 바 업데이트
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