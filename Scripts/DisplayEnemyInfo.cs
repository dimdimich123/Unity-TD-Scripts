using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyInfo : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Text _textName;
    [SerializeField] private Text _textAttributeValues;

    private void Start()
    {
        EnemyInfo info = _enemy.GetComponent<Enemy>().GetInfo();
        _textName.text = info.Name;
        _textAttributeValues.text = $"{info.Speed}\n{info.ArmorType}\n{info.ArmorCount}\n{info.MaxHealth}\n{info.Gold}";
    }
}
