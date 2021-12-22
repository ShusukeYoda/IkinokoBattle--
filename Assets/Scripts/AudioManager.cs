using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    readonly Dictionary<string, AudioClip> clips 
        = new Dictionary<string, AudioClip>();

    #region シングルトン

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
        // instanceが「nullでない」とは、下のコード（instannce == this)が実行済みであることを指す
        // 2回目のAwake呼び出しのため作成しようとしているgameObjectをデストロイする
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // このスクリプトがアタッチされているオブジェクト
        // （今回は、AudioManagerオブジェクト）が違うシーン
        // （DontDestroyOnLoad）に置かれる
        // シーンを遷移しても破棄されなくする
        DontDestroyOnLoad(gameObject);

        // instanceにこのスクリプトのインスタンスを代入する
        // 上記コードのif文で使う。
        instance = this;

        #endregion

        // Assetsフォルダにある「Resourceフォルダ」の中の「2D_SEフォルダ」
        // の中にあるAudioClipを全て取得して配列に代入する
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("2D_SE");

        // 全ての得られたファイルをもとにディクショナリを作成
        // キーはファイル名、値はクリップファイル
        foreach (var clip in audioClips)
        {
            clips.Add(clip.name, clip);
        }
    }

    [SerializeField]
    AudioSource audioSource;

    // サウンド再生
    // 引数は再生するサウンドの名前（ファイル名と同じ）になります
    public void Play(string clipName)
    {
        // ディクショナリに名前がなかったら、例外エラー
        if (!clips.ContainsKey(clipName))
        {
            throw new Exception("Sound " + clipName + " is not defined");
        }

        // クリップに再生音源を代入
        audioSource.clip = clips[clipName];

        // サウンド再生
        audioSource.Play();
    }
}
