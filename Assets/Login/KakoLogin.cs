using UnityEngine;
using UnityEngine.SceneManagement;

public class KakaoLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = LoginValues.GetSessionId();
        string url = "https://localhost:7032/api/login/kakao?session_id=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete 씬에서 로그인을 완료하게 한다
        SceneManager.LoadScene("LoginComplete");
    }
}