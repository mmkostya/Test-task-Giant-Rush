using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    [SerializeField] private TypeColor _type;
    public TypeColor type { get { return _type; } private set { _type = value; } }

    [Space(20)]
    [SerializeField] private PlayerController playerController = null;

    [Tooltip("То что будет изменятся в размере при измененни количества очков")]
    [SerializeField] private Transform transformModel = null;
    
    [Space(20)]
    [Tooltip("Максимальное количество шагов изменения роста при добавлении и отнимании очков")]
    [SerializeField] private int maxLevelScale = 10;

    [Tooltip("Шаг изменения роста при добавлении и отнимании очков")]
    [SerializeField] private float stepScale = 0.1F;

    public int score { get; private set; } = 0;
    public int levelScale { get; private set; } = 0;

    public UnityEvent<int> onUpdatedScore = new UnityEvent<int>();

    private void Start() {
        onUpdatedScore?.Invoke(score);
    }

    public void HandleGem(TypeColor type) {
        //Проверяем тип игрока и камня что он подобрал. Если одинаковы добавляем увеличиваем его размер, иначе уменьшаем
        if (this.type == type) {
            if (levelScale < maxLevelScale) ++levelScale;

            ++score;
            onUpdatedScore?.Invoke(score);
        } else {

            if (levelScale > 0) {
                --levelScale;
            }else if(levelScale == 0) {//Если уменьшать уже некуда, останавливаем движение игрока и запускаем окно GameOver
                playerController.Stop();
                MenuSystem.OpenMenu(MenuSystem.TypeMenu.GameOver);
                return;
            }
            
        }

        //Изменяем размер нашего персонажа
        float newScale = 1F + (stepScale * levelScale);
        transformModel.localScale = new Vector3(newScale, newScale, newScale);
    }


}
