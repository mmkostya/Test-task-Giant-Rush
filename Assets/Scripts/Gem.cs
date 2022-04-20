using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Gem : MonoBehaviour {

    [SerializeField] private TypeColor type;

    private void Start() {
        //Лог о том что система не работает, из-за того что галочка не поставленна
        Collider collider = GetComponent<Collider>();
        if (!collider.isTrigger) {
            Debug.LogError("Collider in gem has isTrigger == false");
        }
    }

    private void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;
        PlayerLinker playerLinker = obj.GetComponent<PlayerLinker>();
        if (playerLinker != null) {
        Player player = playerLinker.player;
            if (player != null) {
                
                //Отправляем в класс игрока что он подобрал какой-то камень, пусть выдаст или заберет себе очки
                player.HandleGem(type);

                //И в конце удаляем объект камня
                Destroy(gameObject);
            }
        }
    }
}
