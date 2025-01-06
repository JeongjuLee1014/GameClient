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
        // ������ ����
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

            // ������ ��û�� ������ ������ ��ٸ��ϴ�.
            yield return request.SendWebRequest();

            // ������ ó���մϴ�.
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("����� �г����� ���������� ������Ʈ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("���� ��û�� �����߽��ϴ�: " + request.error);
            }
        }
    }
}