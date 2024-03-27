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
        //Invoke ����ϴ� ������ 2�� �� ����� ����� ���ļ� 2�� �� ����� ����� ����ó�� ���� �� �� �־

        // �ٽ� ����ϰ� Invoke ����
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        //���� ���� 2���� ����
        Invoke("Disable", clip.length + 2);
    }

    public void Disable()
    {
        // �����ϰ� �־ �������� �ʴ´�
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}