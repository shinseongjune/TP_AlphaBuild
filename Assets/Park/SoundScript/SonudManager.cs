using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonudManager : Singleton<SonudManager>// 싱글톤을 그냥 구현하면 됩니다 싱글톤 안쓰실거면 MonoBehaviour 상속 ㄱㄱ
{
    // 오디오 클립을 재생하기 위한 오디오 소스 리스트
    private List<AudioSource> audioSources = new List<AudioSource>();

    public float basicvolume;

    // 오디오 소스 오브젝트의 수
    public int initialAudioSourceCount = 10;

    void Start()
    {
        // 초기화할 때 AudioSource 오브젝트 생성
        for (int i = 0; i < initialAudioSourceCount; i++)
        {
            GameObject soundObject = new GameObject("AudioSource_" + i);
            soundObject.transform.parent = this.transform;  // SoundManager의 자식으로 추가
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

    // 사운드를 재생하는 함수, 위치가 없으면 0,0,0에 생성
    public void PlaySound(AudioClip clip,float volume = 0, Vector3? position = null)
    {
        // 사용 가능한 오디오 소스를 찾음
        AudioSource availableSource = GetAvailableAudioSource();

        if (availableSource != null)
        {
            // 오디오 소스 위치 설정 (값이 없으면 0,0,0 사용)
            availableSource.transform.position = position ?? Vector3.zero;

            // 오디오 클립 설정 및 재생
            availableSource.clip = clip;
            availableSource.volume = volume == 0 ? basicvolume : volume;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("사용 가능한 오디오 소스가 없습니다.");
        }
    }

    // 사용 가능한 오디오 소스를 찾는 함수
    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)  // 재생 중이지 않은 오디오 소스 찾기
                return audioSource;
        }

        // 모든 오디오 소스가 사용 중일 경우 null 반환
        return null;
    }
}
