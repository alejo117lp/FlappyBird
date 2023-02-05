using UnityEngine;

namespace FlappyBird
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private void Update()
        {
            transform.position += (Vector3.left * Time.deltaTime * speed);
        }
    }
}