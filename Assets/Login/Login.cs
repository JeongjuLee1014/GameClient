using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Login : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = GetSessionId();
        string url = "https://localhost:7032/api/login/kakao?session_id=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete 씬에서 로그인을 완료하게 한다
        SceneManager.LoadScene("LoginComplete");
    }

    private string GetSessionId()
    {
        string sessionId = Guid.NewGuid().ToString();  // 세션 ID를 만든다
        const string filePath = "./Assets/login/loginValues.json";

        string loginValuesString = File.ReadAllText(filePath);
        LoginValues loginValues = JsonUtility.FromJson<LoginValues>(loginValuesString);

        loginValues.sessionId = sessionId;

        string updatedJson = JsonUtility.ToJson(loginValues);
        File.WriteAllText(filePath, updatedJson);

        return sessionId;
    }
}