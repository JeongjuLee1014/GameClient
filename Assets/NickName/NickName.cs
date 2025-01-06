using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    public InputField inputField;

    void Start()
    {
        inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    private void OnInputEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SetNickName(text));
        }
    }

    public IEnumerator SetNickName(string nickName)
    {
        // 서버로 전송
        yield return StartCoroutine(SendPutUserRequest(nickName));
        SceneManager.LoadScene("Home");
    }

    public IEnumerator SendPutUserRequest(string nickName)
    {
        LoginValues loginValues = LoginValues.Get();
        
        string sessionId = loginValues.sessionId;

        User user = new User();
        user.id = 0;
        user.nickName = nickName;
        user.sessionId = sessionId;

        string url = "https://localhost:7032/api/users/session/" + sessionId;

        string jsonData = JsonUtility.ToJson(user);

        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 서버로 요청을 보내고 응답을 기다립니다.
            yield return request.SendWebRequest();

            // 응답을 처리합니다.
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("사용자 닉네임이 성공적으로 업데이트되었습니다.");
            }
            else
            {
                Debug.LogError("서버 요청에 실패했습니다: " + request.error);
            }
        }
    }
}