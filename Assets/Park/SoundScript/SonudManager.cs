using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonudManager : Singleton<SonudManager>// �̱����� �׳� �����ϸ� �˴ϴ� �̱��� �Ⱦ��ǰŸ� MonoBehaviour ��� ����
{
    // ����� Ŭ���� ����ϱ� ���� ����� �ҽ� ����Ʈ
    private List<AudioSource> audioSources = new List<AudioSource>();

    public float basicvolume;

    // ����� �ҽ� ������Ʈ�� ��
    public int initialAudioSourceCount = 10;

    void Start()
    {
        // �ʱ�ȭ�� �� AudioSource ������Ʈ ����
        for (int i = 0; i < initialAudioSourceCount; i++)
        {
            GameObject soundObject = new GameObject("AudioSource_" + i);
            soundObject.transform.parent = this.transform;  // SoundManager�� �ڽ����� �߰�
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
    }

    public void PlayGimmaickSound(AudioClip clip)
    {
        PlaySound(clip);
    }
    public void SetBasicVolume(float volume)
    {
        basicvolume = volume;
    }

    // ���带 ����ϴ� �Լ�, ��ġ�� ������ 0,0,0�� ����
    public void PlaySound(AudioClip clip,float volume = 0, Vector3? position = null)
    {
        // ��� ������ ����� �ҽ��� ã��
        AudioSource availableSource = GetAvailableAudioSource();

        if (availableSource != null)
        {
            // ����� �ҽ� ��ġ ���� (���� ������ 0,0,0 ���)
            availableSource.transform.position = position ?? Vector3.zero;

            // ����� Ŭ�� ���� �� ���
            availableSource.clip = clip;
            availableSource.volume = volume == 0 ? basicvolume : volume;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("��� ������ ����� �ҽ��� �����ϴ�.");
        }
    }

    // ��� ������ ����� �ҽ��� ã�� �Լ�
    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)  // ��� ������ ���� ����� �ҽ� ã��
                return audioSource;
        }

        // ��� ����� �ҽ��� ��� ���� ��� null ��ȯ
        return null;
    }
}
