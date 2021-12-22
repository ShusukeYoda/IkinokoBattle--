using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CancelButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    // ボタン押下時にキャンセルの音が鳴るようにする
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("cancel");
        });
    }
}
