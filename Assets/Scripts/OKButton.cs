using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OKButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    // ƒ{ƒ^ƒ“‰Ÿ‰ºŽž‚É‚n‚j‚Ì‰¹‚ª–Â‚é‚æ‚¤‚É‚·‚é
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("ok");
        });
    }
}
