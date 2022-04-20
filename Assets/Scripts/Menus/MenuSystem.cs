using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour {
    private static MenuSystem instance = null;

    [System.Serializable]
    private class DataMenu {
        #if UNITY_EDITOR
            //Для удобства, чтоб в редакторе отображалась надпись, а не Element0...999
            [SerializeField] private string name;
        #endif

        public TypeMenu typeMenu;
        public MenuBase menu;
    }

    public enum TypeMenu { GuiGame, Main, GameOver }

    [Tooltip("Какое меню открыть на старте игры")]
    [SerializeField] private TypeMenu openMenuOnStart = TypeMenu.Main;

    [Tooltip("Перечисление всех меню и их типов")]
    [SerializeField] private List<DataMenu> allMenus = new List<DataMenu>();

    private MenuBase openedMenu = null;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Created double class MenuSystem");
            Destroy(gameObject);
            return;
        }

        instance = this;

        //Открываем и закрываем все меню, чтоб инициализировались.
        foreach (DataMenu dataMenu in allMenus) {
            dataMenu.menu.Open();
            dataMenu.menu.Close();
        }
    }

    private void Start() {
        OpenMenu(openMenuOnStart);
    }

    public static void OpenMenu(TypeMenu newTypeMenu) {
        if (instance.openedMenu != null) {
            instance.openedMenu.Close();
            instance.openedMenu = null;
        }

        MenuBase menu = instance.GetMenu(newTypeMenu);

        if (menu != null) {
            menu.Open();
            instance.openedMenu = menu;
        }

    }

    private MenuBase GetMenu(TypeMenu typeMenu) {
        foreach (DataMenu dataMenu in allMenus) {
            if (dataMenu.typeMenu == typeMenu) return dataMenu.menu;
        }
        return null;
    }

}
