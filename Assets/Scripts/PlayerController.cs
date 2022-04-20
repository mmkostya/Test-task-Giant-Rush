using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Tooltip("Скорость бега вперед")]
    [SerializeField] private float speedMove = 100f;

    [Tooltip("Скорость бега по сторонам")]
    [SerializeField] private float speedSideMove = 100f;

    [SerializeField] private ASwaperBaseController swaperController = null;
    
    [Space(20)]
    [SerializeField] private Rigidbody rigidBody = null;

    private bool isRunned = false;

    public void Run() {
        isRunned = true;
    }

    public void Stop() {
        isRunned = false;
        rigidBody.velocity = Vector3.zero;
    }

    private void FixedUpdate() {
        if (isRunned) {
            Vector3 velocity = rigidBody.velocity;

            rigidBody.velocity = new Vector3((swaperController.Horizontal * speedSideMove), velocity.y, (speedMove * 0.01F));
        }
    }

}
