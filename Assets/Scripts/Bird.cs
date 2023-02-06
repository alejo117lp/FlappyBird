using UnityEngine;

namespace FlappyBird
{
    public class Bird : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb2D;
        [SerializeField, Range(0, 10)]
        private float speed;

        private void Awake()
        {
            if (rb2D == null)
                rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (GameManager.Instance.isGameOver) return;
#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                Move();
#endif

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
                Move();
#endif
        }

        private void Move()
        {
            if (rb2D != null)
                rb2D.velocity = Vector2.up * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ground")) {
                GameManager.Instance.GameOver();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("PipeTrigger")) {
                GameManager.Instance.IncreaseScore();
            }
        }
    }
}