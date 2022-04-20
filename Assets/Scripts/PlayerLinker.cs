using UnityEngine;

//Класс который просто передает ссылку на игрока. Чтоб не делать статическую переменную игрока + какая-никакая пометка что это игрок, вместо строкового тега
public class PlayerLinker : MonoBehaviour {

    [SerializeField] private Player _player = null;
    public Player player { get { return _player; } }
}
