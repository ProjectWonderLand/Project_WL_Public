using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    public AudioSource bgmSource;
    public List<AudioSource> sfxSources;
    public int maxSFXCount = 20; // ���� ��� ������ SFX ��
    private int currentSFXIndex = 0;
    private int currentPlayingSFXCount = 0; // ���� ��� ���� SFX ��
    private float bgmVolume = 0.5f;// BGM ������ ������ ����(�ʱ� ���� 0.5)
    private float sfxVolume = 0.3f; // SFX ������ ������ ����(�ʱ� ���� 0.3)

    public List<AudioClip> bgmClips;
    public List<AudioClip> sfxClips;

    protected override void Awake()
    {
        base.Awake();
        InitializeAudioSource();
    }

    private void InitializeAudioSource()
    {
        // BGM AudioSource �ʱ�ȭ
        GameObject bgmObject = new GameObject("BGMSource");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.volume = bgmVolume; // �ʱ� ���� ����
        bgmSource.loop = true;



        // SFX AudioSource �ʱ�ȭ
        sfxSources = new List<AudioSource>();

        for (int i = 0; i < maxSFXCount; i++)
        {
            GameObject sfxObject = new GameObject($"SFXSource_{i}");
            sfxObject.transform.parent = transform;
            AudioSource sfxSource = sfxObject.AddComponent<AudioSource>();
            sfxSource.volume = sfxVolume; // �ʱ� ���� ����
            sfxSource.loop = false;
            sfxSources.Add(sfxSource);
        }
    }

    public void PlayBGM(string bgmName)
    {
        AudioClip bgmClip = bgmClips.Find(clip => clip.name == bgmName);
        
        // bgmClip�� ������ return
        if (bgmClip == null)
        {
            return;
        }

        float targetVolume = bgmSource.volume;

        if (bgmSource.isPlaying)
        {
            StartCoroutine(FadeOutBGM(bgmSource, 1f, () => { bgmSource.clip = bgmClip; StartCoroutine(FadeInBGM(bgmSource, 1f, targetVolume)); }));
        }
        else
        {
            bgmSource.clip = bgmClip;
            StartCoroutine(FadeInBGM(bgmSource, 1f, targetVolume));
        }
    }

    public void PlaySFX(string sfxName, float pitch = 1f)
    {
        // ���� ��� ���� SFX�� ���� �ִ�ġ�� �ʰ��ϸ� ���ο� SFX ����� ����
        if (currentPlayingSFXCount >= maxSFXCount)
        {
            return;
        }

        AudioClip sfxClip = sfxClips.Find(clip => clip.name == sfxName);
        if (sfxClip == null)
        {
            return;
        }

        AudioSource sfxSource = sfxSources[currentSFXIndex]; // ���� ���� ����
        float originalPitch = sfxSource.pitch; // ���� pitch ���� ����

        // pitch ���� ����
        sfxSource.pitch = pitch;

        // �� Clip ������
        sfxSource.clip = sfxClip;
        sfxSource.Play();

        // ��� ���� SFX �� ����
        currentPlayingSFXCount++;
        StartCoroutine(CheckIfPlaying(sfxSource, originalPitch));

        // �ε����� �������� �̵�, �ִ� �ε����� �����ϸ� �ٽ� 0���� �ǵ��ư���.
        currentSFXIndex = (currentSFXIndex + 1) % maxSFXCount;
    }

    // System.Action : �Ű������� ���� ��ȯ���� ���� delegateŸ������, �ڵ��� Ư�� �κп��� Ư�� �۾��� �����ϱ� ���� ���.
    // �޼���, �͸� �޼���, ���ٽ� �� � �ڵ� �����̵� ���� �� ������ �Ʒ������� ���̵� �ƿ��� �Ϸ�Ǿ��� �� ����� �۾����� ���ٽ����� �������־���.
    private IEnumerator FadeOutBGM(AudioSource audioSource, float duration, System.Action onComplete)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
        onComplete?.Invoke();
    }

    private IEnumerator FadeInBGM(AudioSource audioSource, float duration, float targetVolume)
    {
        audioSource.volume = 0;
        audioSource.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    // BGM ���� ���� �޼���
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // SFX ���� ���� �޼���
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume; // ���� ���� ������Ʈ
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }

    private IEnumerator CheckIfPlaying(AudioSource source, float originalPitch)
    {
        while (source.isPlaying)
        {
            yield return null;
        }

        // ����� ���� �� pitch ���� ������� ����
        source.pitch = originalPitch;
        currentPlayingSFXCount--;
    }
}