using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OKButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    // �{�^���������ɂn�j�̉�����悤�ɂ���
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("ok");
        });
    }
}
