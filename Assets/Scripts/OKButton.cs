using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OKButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    // ボタン押下時にＯＫの音が鳴るようにする
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("ok");
        });
    }
}
