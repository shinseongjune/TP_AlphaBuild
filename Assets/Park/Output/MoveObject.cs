using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : GimmickOutput
{
    /*
    public Vector3 targetPos; // ��ǥ ��ġ
    public Quaternion targetRotation; // ��ǥ ȸ��
    public AnimationCurve moveCurve; // �̵� �ִϸ��̼� Ŀ��
    public AnimationCurve rotationCurve; // ȸ�� �ִϸ��̼� Ŀ��
    public float moveDuration = 2.0f; // �̵� �� ȸ���� �ɸ��� �ð�

    private Vector3 startPos; // ���� ��ġ
    private Quaternion startRotation; // ���� ȸ��

    public bool isDone;

    private void Start()
    {
        isDone = false;
    }

    public override void Act()
    {
        if (!isDone)
        {
            // ���� ������Ʈ�� ��ġ�� ȸ���� ���۰����� ����
            startPos = transform.position;
            startRotation = transform.rotation;

            targetPos = transform.position + targetPos;
            // �ڷ�ƾ ����

            StartCoroutine(Movement());
            isDone = true;
        }
    }

    public IEnumerator Movement()
    {
        float elapsedTime = 0f; // ��� �ð� ����

        // �̵� �� ȸ���� �Ϸ�� ������ �ݺ�
        while (elapsedTime < moveDuration)
        {
            // ��� �ð� ���� (0���� 1 ������ ��)
            float t = elapsedTime / moveDuration;

            // �̵� �ִϸ��̼� Ŀ�긦 �̿��Ͽ� ��ġ ����
            float moveCurveValue = moveCurve.Evaluate(t);
            float newX =  Mathf.Lerp(startPos.x, targetPos.x, moveCurveValue);
            float newY =  Mathf.Lerp(startPos.y,  targetPos.y, moveCurveValue);
            float newZ =  Mathf.Lerp(startPos.z,  targetPos.z, moveCurveValue);
            transform.position = new Vector3(newX, newY, newZ);

            // ȸ�� �ִϸ��̼� Ŀ�긦 �̿��Ͽ� ȸ�� ����
            float rotationCurveValue = rotationCurve.Evaluate(t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationCurveValue);

            // ��� �ð� ����
            elapsedTime += Time.deltaTime;

            // �� ������ ��ٸ�
            yield return null;
        }

        // �̵� �� ȸ�� �Ϸ� �� ��Ȯ�� ��ǥ ��ġ �� ȸ������ ����
        transform.position = new Vector3(
           targetPos.x ,
           targetPos.y ,
           targetPos.z 
        );
        transform.rotation = targetRotation;
    }
    */
    [Header("Position Settings")]
    [HideInInspector] public Vector3 startPosition;  // ���� ��ġ
    public Vector3 endPosition;  // ��ǥ ��ġ

    [Header("Rotation Settings")]
    [HideInInspector] public Vector3 startRotation;  // ���� ȸ�� (Euler angles)
    public Vector3 endRotation;    // ��ǥ ȸ�� (Euler angles)

    [Header("Animation Settings")]
    public AnimationCurve movementCurve;  // �̵� �ִϸ��̼� Ŀ��
    public AnimationCurve rotationCurve;  // ȸ�� �ִϸ��̼� Ŀ��
    public float duration = 2f;  // �ִϸ��̼� ���� �ð�

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // �ʱ� ��ġ �� ȸ���� ����
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        isDone = false;
    }

    // �ִϸ��̼��� �����ϴ� �Լ� (�ܺο��� ȣ��)
    public override void Act()
    {
        if (!isDone)
        {
            // ���� ������Ʈ�� ��ġ�� ȸ���� ���۰����� ����
            startPosition = transform.position;
            startRotation = transform.eulerAngles;

            // �ڷ�ƾ ����
            StartCoroutine(AnimateMovement(transform.position + endPosition, transform.eulerAngles + endRotation));
        }
    }

    // Coroutine�� ���� �ִϸ��̼� ó��
    private IEnumerator AnimateMovement(Vector3 targetPos, Vector3 targetRotation)
    {
        float timer = 0f;

        // ���� ��ġ �� ȸ�� ����
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // �ִϸ��̼� ����
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);  // 0���� 1���� ����� ���

            // �ִϸ��̼� Ŀ�� ����
            float curveValue = movementCurve.Evaluate(progress);
            float rotationCurveValue = rotationCurve.Evaluate(progress);

            // ��ġ ����
            transform.position = Vector3.Lerp(initialPosition, targetPos, curveValue);

            // ��� ���� ȸ���� ����
            Quaternion startRot = initialRotation;
            Quaternion endRot = Quaternion.Euler(targetRotation);
            transform.rotation = Quaternion.Lerp(startRot, endRot, rotationCurveValue);

            yield return null;  // �� ������ ���
        }

        // �ִϸ��̼��� ���� �� ��Ȯ�� ��ǥ ��ġ �� ȸ������ ����
        transform.position = targetPos;
        transform.rotation = Quaternion.Euler(targetRotation);

        isDone = true;
    }
}
