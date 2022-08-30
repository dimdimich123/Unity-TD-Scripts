using UnityEngine.UI;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private Enemy _enemyParent;
    private float _maxHp;
    private Image _image;

    void Start()
    {
        _enemyParent = transform.parent.parent.GetComponent<Enemy>();
        _image = GetComponent<Image>();
        _enemyParent._onTakeDamage += SetHealthValue;
    }

    private void SetHealthValue(float value)
    {
        _image.fillAmount = value;
        if(value <= 0) transform.parent.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _enemyParent._onTakeDamage -= SetHealthValue;
    }
}
