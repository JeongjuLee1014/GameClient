using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class LoginComplete : MonoBehaviour
{
    private User user;
    private bool isLoginCompleted = false;

    public void OnMouseDown()
    {
        StartCoroutine(HandleLoginCompletion());
    }

    private IEnumerator HandleLoginCompletion()
    {
        while (isLoginCompleted == false)
        {
            Debug.Log("로그인 처리 중입니다.");
            yield return StartCoroutine(CheckLoginCompleted());
        }

        SetLogined();

        if (string.IsNullOrEmpty(user.nickName))
        {
            SceneManager.LoadScene("NickName");
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void SetLogined()
    {
        const string filePath = "./Assets/login/loginValues.json";

        string loginValuesString = File.ReadAllText(filePath);
        LoginValues loginValues = JsonUtility.FromJson<LoginValues>(loginValuesString);

        loginValues.isLogined = true;

        string updatedJson = JsonUtility.ToJson(loginValues);
        File.WriteAllText(filePath, updatedJson);
    }

    public IEnumerator CheckLoginCompleted()
    {
        yield return StartCoroutine(GetUser());
        isLoginCompleted = (user != null);
    }

    public IEnumerator GetUser()
    {
        const string filePath = "./Assets/login/loginValues.json";

        // 파일이 존재하지 않을 경우 처리
        if (!File.Exists(filePath))
        {
            Debug.LogError("Login values file not found: " + filePath);
            yield break;
        }

        string loginValuesString = File.ReadAllText(filePath);
        LoginValues loginValues = JsonUtility.FromJson<LoginValues>(loginValuesString);

        yield return StartCoroutine(SendGetUserRequest(loginValues.sessionId));
    }

    public IEnumerator SendGetUserRequest(string sessionId)
    {
        string url = "https://localhost:7032/api/users/session/" + sessionId;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                user = JsonUtility.FromJson<User>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("GET request failed: " + request.error);
                user = null;
            }
        }
    }
}