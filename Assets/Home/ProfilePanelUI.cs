using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelUI : MonoBehaviour
{
    //[SerializeField] private Image profileImage;   // ������ �̹���
    [SerializeField] private TMP_Text nicknameText; // �г��� �ؽ�Ʈ
    [SerializeField] private Slider expSlider;      // ����ġ ��
    //[SerializeField] private TMP_Text expText;      // ����ġ ��ġ �ؽ�Ʈ

    // ����ġ ���� ������
    private int currentExp = 0;
    private int maxExp = 100; // �ִ� ����ġ (�⺻��, �������� �޾ƿ� ���� ����)

    private void Start()
    {
        // �ʱ� ������ ����
        UpdateProfileUI();
    }

    private void Update()
    {
        // �ǽð� ������ ���� ���� �� ������Ʈ (�ʿ��)
        if (User.Instance != null)
        {
            UpdateProfileUI();
        }
    }

    // �������� �г��� ������Ʈ
    public void UpdateNickname(string newNickname)
    {
        User.Instance.nickName = newNickname;
        UpdateProfileUI();
    }

    // �������� ����ġ ������ ������Ʈ
    // <param name="newExp">���� ����ġ</param>
    // <param name="newMaxExp">�ִ� ����ġ</param>
    public void UpdateExp(int newExp, int newMaxExp)
    {
        currentExp = newExp;
        maxExp = newMaxExp;
        UpdateProfileUI();
    }

    // UI ������Ʈ �޼���
    public void UpdateProfileUI()
    {
        // User.Instance �����ͷ� UI ������Ʈ
        nicknameText.text = User.Instance.nickName;

        // ����ġ �� �������� (�ӽ� �� -> ���� ������ �ݿ� ����)
        //currentExp = User.Instance.numStars; // ����: numStars�� ����ġ�� ���
        currentExp = 50;
        maxExp = 100; // ���� ��, �ʿ�� �������� ���� ����

        // ����ġ �� ������Ʈ
        expSlider.value = (float)currentExp / maxExp;
        //expText.text = $"{currentExp}/{maxExp}";
    }
}
