using UnityEngine;

namespace FlappyBird
{
    public class Bird : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rigidbody2D;
        [SerializeField, Range(0, 10)]
        private float speed;

        private void Awake()
        {
            if (rigidbody2D == null)
                rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
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
            if (rigidbody2D != null)
                rigidbody2D.velocity = Vector2.up * speed;
        }
    }
}