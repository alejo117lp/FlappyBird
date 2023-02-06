using System.Collections;
using UnityEngine;

namespace FlappyBird
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject pipe;
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private float timeToSpawnFirstPipe;
        [SerializeField]
        private float timeToSpawnPipe;
        [SerializeField, Range(-1, 1)]
        float minHeight, maxHeight;

        private void Start()
        {
            StartCoroutine(SpawnPipes());
        }

        private Vector3 GetSpawnPosition() {
            return new Vector3
                (spawnPoint.position.x, Random.Range(minHeight, maxHeight), spawnPoint.position.z);
        }

        private IEnumerator SpawnPipes()
        {
            yield return new WaitForSeconds(timeToSpawnFirstPipe);

            Instantiate(pipe, GetSpawnPosition(), Quaternion.identity);

            do
            {
                yield return new WaitForSeconds(timeToSpawnPipe);

                Instantiate(pipe, GetSpawnPosition(), Quaternion.identity);
            } while (true);
        }

        public void Stop() {
            StopAllCoroutines();
        }
    }
}