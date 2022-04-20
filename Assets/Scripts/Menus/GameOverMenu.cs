using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MenuBase {

    [SerializeField] private Text textScore = null;

    [SerializeField] private Player player = null;

    public void OnClickBtnPlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void Open() {
        textScore.text = player.score.ToString();

        base.Open();
    }

}
