using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuBase {

    [SerializeField] private PlayerController playerController = null;

    public void OnClickBtnPlay() {
        MenuSystem.OpenMenu(MenuSystem.TypeMenu.GuiGame);
        playerController.Run();
    }

}
