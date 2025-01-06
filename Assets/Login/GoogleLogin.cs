using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GoogleLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = GetSessionId();
        string url = "https://localhost:7032/api/login/google?session_id=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete 씬에서 로그인을 완료하게 한다
        SceneManager.LoadScene("LoginComplete");
    }

    private string GetSessionId()
    {
        string sessionId = Guid.NewGuid().ToString();  // 세션 ID를 만든다
        
        LoginValues loginValues = LoginValues.Get();

        loginValues.sessionId = sessionId;

        LoginValues.Set(loginValues);
        
        return sessionId;
    }
}