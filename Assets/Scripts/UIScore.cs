using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIScore : MonoBehaviour {

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    public void SetScore(int score) {
        text.text = score.ToString();
    }

}
