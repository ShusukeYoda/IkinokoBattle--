using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // インスペクタでアウトレット接続する

    [SerializeField]
    Button pauseButton;
    [SerializeField]
    Button resumeButton;
    [SerializeField]
    Button itemButton;
    [SerializeField]
    Button recipeButton;
    [SerializeField]
    GameObject pausePanel;

    void Start()
    {
        // 最初にポーズ画面を非表示にする
        pausePanel.SetActive(false);

        // 各ボタンが押された時のイベントハンドラ
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        itemButton.onClick.AddListener(ToggleItemDialog);
        recipeButton.onClick.AddListener(ToggleRecipeDialog);
    }
    private void Pause()
    {
        //時間の進み具合を調整できますが、Update()は実行されます。
        //0だと時間が停止する。
        //Updateで反映させるため(例えば移動)には、Time.deltaTimeを乗ずる。
        //FixedUpdataは影響を受ける
        Time.timeScale = 0;
        //パネルを表示
        pausePanel.SetActive(true);
    }
    // 再開(Resume)
    private void Resume()
    {
        //通常の時間の流れ
        Time.timeScale = 1;
        //パネルを非表示
        pausePanel.SetActive(false);
    }

    [SerializeField] ItemDialog itemsDialog;
    //トグルとは、ある同じ操作を繰り返すことで、
    //機能や状態のON/OFFを切り替える仕組みのこと
    private void ToggleItemDialog()
    {
        itemsDialog.Toggle();
    }
    private void ToggleRecipeDialog()
    {
        //ToDo
    }
}
