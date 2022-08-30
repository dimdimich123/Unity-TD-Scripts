using UnityEngine;
using UnityEngine.UI;

public class InfoTableController : MonoBehaviour
{
    [SerializeField] private Image _button;
    [SerializeField] private Sprite _arrowUp;
    [SerializeField] private Sprite _arrowDown;
    private float _height;
    private bool isHidden = false;

    private void Start()
    {
        _height = GetComponent<RectTransform>().rect.height;
    }

    public void ChangePanel()
    {
        if(isHidden)
        {
            transform.localPosition += new Vector3(0, _height, 0);
            isHidden = false;
            _button.sprite = _arrowDown;
        }
        else
        {
            transform.localPosition -= new Vector3(0, _height, 0);
            isHidden = true;
            _button.sprite = _arrowUp;
        }
    }
}
