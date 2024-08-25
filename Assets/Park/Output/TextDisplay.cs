using System.Collections;
using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    [Header("������Ʈ�� Ȱ��ȭ �� �� �ؽ�Ʈ �����ϱ�")]
    public bool isEnableActing;
    TextMeshPro textUI;  // UI�� ����ϴ� ��� TextMeshProUGUI�� �����ؾ� ��

    private void Start()
    {
        // TextMeshProUGUI�� ����Ѵٸ� �� �κ��� TextMeshProUGUI�� ����
        textUI = GetComponent<TextMeshPro>();

        // ���� UI �ؽ�Ʈ�� ���
        // textUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (isEnableActing)
        {
            Act("Hello, World!", 0, 0.05f);  // ������Ʈ�� Ȱ��ȭ�� �� �⺻ ������ �ؽ�Ʈ ǥ��
        }
    }

    public  void Act(string message = "", int number = 0, float value = 0f)
    {
        // StartCoroutine�� �ùٸ��� ȣ��
        StartCoroutine(TypeText(message, value));
    }

    // �ڷ�ƾ���� Ÿ���� ȿ�� ����
    private IEnumerator TypeText(string fullText, float typingSpeed)
    {
        textUI.text = "";  // ó������ �� �ؽ�Ʈ�� ����

        // ���ڿ��� ���������� �� ���ھ� �߰�
        for (int i = 0; i < fullText.Length; i++)
        {
            textUI.text += fullText[i];  // �� ���ھ� �߰�
            yield return new WaitForSeconds(typingSpeed);  // ������ �ð���ŭ ���
        }
    }
}
