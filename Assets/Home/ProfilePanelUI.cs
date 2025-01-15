using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelUI : MonoBehaviour
{
    //[SerializeField] private Image profileImage;   // 프로필 이미지
    [SerializeField] private TMP_Text nicknameText; // 닉네임 텍스트
    [SerializeField] private Slider expSlider;      // 경험치 바
    //[SerializeField] private TMP_Text expText;      // 경험치 수치 텍스트

    // 경험치 관련 데이터
    private int currentExp = 0;
    private int maxExp = 100; // 최대 경험치 (기본값, 서버에서 받아올 수도 있음)

    private void Start()
    {
        // 초기 데이터 설정
        UpdateProfileUI();
    }

    private void Update()
    {
        // 실시간 데이터 변경 감지 및 업데이트 (필요시)
        if (User.Instance != null)
        {
            UpdateProfileUI();
        }
    }

    // 서버에서 닉네임 업데이트
    public void UpdateNickname(string newNickname)
    {
        User.Instance.nickName = newNickname;
        UpdateProfileUI();
    }

    // 서버에서 경험치 데이터 업데이트
    // <param name="newExp">현재 경험치</param>
    // <param name="newMaxExp">최대 경험치</param>
    public void UpdateExp(int newExp, int newMaxExp)
    {
        currentExp = newExp;
        maxExp = newMaxExp;
        UpdateProfileUI();
    }

    // UI 업데이트 메서드
    public void UpdateProfileUI()
    {
        // User.Instance 데이터로 UI 업데이트
        nicknameText.text = User.Instance.nickName;

        // 경험치 값 가져오기 (임시 값 -> 서버 데이터 반영 가능)
        //currentExp = User.Instance.numStars; // 예시: numStars를 경험치로 사용
        currentExp = 50;
        maxExp = 100; // 임의 값, 필요시 서버에서 설정 가능

        // 경험치 바 업데이트
        expSlider.value = (float)currentExp / maxExp;
        //expText.text = $"{currentExp}/{maxExp}";
    }
}
