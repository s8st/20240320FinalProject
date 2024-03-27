using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        //Invoke 취소하는 이유는 2초 후 끄라는 명령이 겹쳐서 2초 후 끄라는 명령이 예약처럼 실행 될 수 있어서

        // 다시 등록하고 Invoke 실행
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        //지연 실행 2초후 끄기
        Invoke("Disable", clip.length + 2);
    }

    public void Disable()
    {
        // 재사용하고 있어서 삭제하지 않는다
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}