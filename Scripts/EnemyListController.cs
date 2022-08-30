using UnityEngine;

public class EnemyListController : MonoBehaviour
{
    private Transform[] _enemies;
    private int _index = 0;

    void Start()
    {
        _enemies = new Transform[transform.childCount];
        int index = 0;
        foreach(Transform child in transform)
        {
            _enemies[index] = child;
            index++;
        }
    }

    public void ButtonClick(int direction)
    {
        _enemies[_index].gameObject.SetActive(false);
        _index += direction;
        if(_index >= _enemies.Length) _index = 0;
        if(_index < 0) _index = _enemies.Length - 1;
        _enemies[_index].gameObject.SetActive(true);
    }
}
