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

        private void Start()
        {
            StartCoroutine(SpawnPipes());
        }

        private IEnumerator SpawnPipes()
        {
            yield return new WaitForSeconds(timeToSpawnFirstPipe);

            Instantiate(pipe, spawnPoint.position, Quaternion.identity);

            do
            {
                yield return new WaitForSeconds(timeToSpawnPipe);

                Instantiate(pipe, spawnPoint.position, Quaternion.identity);
            } while (true);
        }
    }
}