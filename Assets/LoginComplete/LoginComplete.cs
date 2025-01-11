using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginComplete : MonoBehaviour
{
    private bool isLoginCompleted = false;

    public void OnMouseDown()
    {
        StartCoroutine(HandleLoginCompletion());
    }

    private IEnumerator HandleLoginCompletion()
    {
        while (!isLoginCompleted)
        {
            Debug.Log("로그인 처리 중...");
            yield return StartCoroutine(CheckLoginCompleted());
        }

        SetLogined();

        Debug.Log($"NickName: {User.Instance.nickName}"); // 닉네임 값 확인

        // 닉네임이 비어 있으면 NickName 씬으로 이동
        if (string.IsNullOrEmpty(User.Instance.nickName))
        {
            SceneManager.LoadScene("NickName");
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }

    private void SetLogined()
    {
        LoginValues loginValues = LoginValues.Get();
        loginValues.isLogined = true;
        LoginValues.Set(loginValues);
    }

    private IEnumerator CheckLoginCompleted()
    {
        yield return StartCoroutine(GetUser());
        Debug.Log($"User ID: {User.Instance.id}"); // User ID 확인
        isLoginCompleted = !string.IsNullOrEmpty(User.Instance.id);
    }

    private IEnumerator GetUser()
    {
        LoginValues loginValues = LoginValues.Get();
        string url = $"{Constant.SERVER_URL}/api/users/session/{loginValues.sessionId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Response data: {request.downloadHandler.text}"); // 응답 데이터 확인

                // Singleton User 인스턴스에 데이터 저장
                User loadedUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                Debug.Log($"Loaded User Data: Id={loadedUser.id}, NickName={loadedUser.nickName}, SessionId={loadedUser.sessionId}");

                User.Instance.UpdateUserData(
                    loadedUser.id,
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
                Debug.LogError("Failed to get user data: " + request.error);
            }
        }
    }
}
