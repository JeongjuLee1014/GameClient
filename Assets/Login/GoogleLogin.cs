using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoogleLogin : MonoBehaviour
{
    //public void OnMouseDown()
    public void OnGoogleLoginClick()
    {
        string sessionId = LoginValues.CreateSessionId();

        LoginValues loginValues = LoginValues.Get();
        loginValues.sessionId = sessionId;
        LoginValues.Set(loginValues);

        //string url = Constant.SERVER_URL + "/api/login/google?sessionId=" + sessionId;
        //URL 인코딩
        string url = $"{Constant.SERVER_URL}/api/login/google?sessionId={Uri.EscapeDataString(sessionId)}";

        Application.OpenURL(url);

        // LoginComplete 씬에서 로그인을 완료하게 한다
        SceneManager.LoadScene("LoginComplete");
    }

}