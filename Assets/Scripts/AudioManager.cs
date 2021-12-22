using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    readonly Dictionary<string, AudioClip> clips 
        = new Dictionary<string, AudioClip>();

    #region �V���O���g��

    static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        // instance���unull�łȂ��v�Ƃ́A���̃R�[�h�iinstannce == this)�����s�ς݂ł��邱�Ƃ��w��
        // 2��ڂ�Awake�Ăяo���̂��ߍ쐬���悤�Ƃ��Ă���gameObject���f�X�g���C����
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // ���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g
        // �i����́AAudioManager�I�u�W�F�N�g�j���Ⴄ�V�[��
        // �iDontDestroyOnLoad�j�ɒu�����
        // �V�[����J�ڂ��Ă��j������Ȃ�����
        DontDestroyOnLoad(gameObject);

        // instance�ɂ��̃X�N���v�g�̃C���X�^���X��������
        // ��L�R�[�h��if���Ŏg���B
        instance = this;

        #endregion

        // Assets�t�H���_�ɂ���uResource�t�H���_�v�̒��́u2D_SE�t�H���_�v
        // �̒��ɂ���AudioClip��S�Ď擾���Ĕz��ɑ������
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("2D_SE");

        // �S�Ă̓���ꂽ�t�@�C�������ƂɃf�B�N�V���i�����쐬
        // �L�[�̓t�@�C�����A�l�̓N���b�v�t�@�C��
        foreach (var clip in audioClips)
        {
            clips.Add(clip.name, clip);
        }
    }

    [SerializeField]
    AudioSource audioSource;

    // �T�E���h�Đ�
    // �����͍Đ�����T�E���h�̖��O�i�t�@�C�����Ɠ����j�ɂȂ�܂�
    public void Play(string clipName)
    {
        // �f�B�N�V���i���ɖ��O���Ȃ�������A��O�G���[
        if (!clips.ContainsKey(clipName))
        {
            throw new Exception("Sound " + clipName + " is not defined");
        }

        // �N���b�v�ɍĐ���������
        audioSource.clip = clips[clipName];

        // �T�E���h�Đ�
        audioSource.Play();
    }
}
