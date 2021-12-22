using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CancelButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    // �{�^���������ɃL�����Z���̉�����悤�ɂ���
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("cancel");
        });
    }
}
