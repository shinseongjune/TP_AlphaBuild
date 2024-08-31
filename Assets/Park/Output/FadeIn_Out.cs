using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn_Out : GimmickOutput
{
    public enum FadeType { FadeIn, FadeOut }

    public float duration = 1f; // ���̵� ȿ���� ���� �ð�
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // �⺻ �ִϸ��̼� Ŀ��
    public FadeType fadeType; // ���̵� �� �Ǵ� ���̵� �ƿ� ����

    public Image targetImage; // ���̵带 ������ UI �̹��� ������Ʈ

    public override void Act()
    {
        // targetImage�� �Ҵ���� ���� ��� ��� ����մϴ�.
        if (targetImage == null)
        {
            Debug.LogError("targetImage�� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� UI Image�� �Ҵ��ϼ���.");
            return;

        }

        // ���̵� ȿ���� �����մϴ�.
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        float elapsedTime = 0f;
        float startAlpha = fadeType == FadeType.FadeIn ? 0f : 1f;
        float endAlpha = fadeType == FadeType.FadeIn ? 1f : 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);
            float alpha = fadeCurve.Evaluate(normalizedTime);

            Color color = targetImage.color;
            color.a = Mathf.Lerp(startAlpha, endAlpha, alpha);
            targetImage.color = color;

            yield return null;
        }

        // ���� ���� ���� ��Ȯ�� �����ǵ��� �����մϴ�.
        Color finalColor = targetImage.color;
        finalColor.a = endAlpha;
        targetImage.color = finalColor;
    }
}
