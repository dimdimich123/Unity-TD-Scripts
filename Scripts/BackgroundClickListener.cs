using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundClickListener : MonoBehaviour, IPointerDownHandler
{
    public static void DisableAllMenu()
    {
        if(TowerMenu.ActiveMenu != null)
        {
            TowerMenu.ActiveMenu.SetActive();
            TowerMenu.ActiveMenu = null;
        }

        if(CallUpBuildingMenu.ActiveMenu != null)
        {
            CallUpBuildingMenu.ActiveMenu.SetActiveFalse();
            CallUpBuildingMenu.ActiveMenu = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DisableAllMenu();
    }
}
