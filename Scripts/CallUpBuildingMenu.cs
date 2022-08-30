using UnityEngine;
using UnityEngine.EventSystems;

public class CallUpBuildingMenu : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private BuildingPanel BuildMenu;
    [SerializeField] private AudioSource _audio;


    public static CallUpBuildingMenu ActiveMenu = null;

    public void SetActiveFalse() => BuildMenu.gameObject.SetActive(false);

    public void PlaySellTowerSound() => _audio.Play();

    public void OnPointerDown(PointerEventData eventData)
    {
        if(TowerMenu.ActiveMenu != null)
        {
            TowerMenu.ActiveMenu.SetActive();
            TowerMenu.ActiveMenu = null;
        }

        if(ActiveMenu == this)
        //if(BuildMenu.gameObject.activeSelf)
        {
            BuildMenu.gameObject.SetActive(false);
            ActiveMenu = null;
        }
        else
        {
            BuildMenu.GetComponent<BuildingPanel>().SetPosition(transform.position, gameObject);
            BuildMenu.gameObject.SetActive(true);
            ActiveMenu = this;
        }
    }
}
