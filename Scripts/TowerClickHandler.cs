using UnityEngine;
using UnityEngine.EventSystems;

public class TowerClickHandler : MonoBehaviour, IPointerDownHandler
{
    private TowerMenu _towerMenu;

    private void Awake()
    {
        _towerMenu = GetComponentInParent<TowerMenu>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _towerMenu.SetActive();
        TowerInfo info = GetComponentInParent<Tower>().Info;
        LevelManager.Current.OnTowerClickInvoke(info);
    }
}
