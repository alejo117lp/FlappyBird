using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePool : MonoBehaviour
{
    [SerializeField] GameObject pipePrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] List<GameObject> pipeList;

    static PipePool instance;
    public static PipePool Instance { get { return instance; } }

    private void Awake() {
       if(instance == null) {
            instance = this;
       }
       else {

            Destroy(gameObject);
       }
    }

    void Start()
    {
        AddPipesToPool(poolSize);
    }

    void AddPipesToPool(int amount) {
        for (ushort i = 0; i < poolSize; i++) {
            GameObject pipe = Instantiate(pipePrefab);
            pipe.SetActive(false);
            pipeList.Add(pipe);
            pipe.transform.parent = transform;
        }
    }

    public GameObject RequestPipe() {
        for(int i = 0; i < pipeList.Count; i++) {
            if (!pipeList[i].activeSelf) {
                pipeList[i].SetActive(true);
                return pipeList[i];
            }
        }
        AddPipesToPool(1);
        pipeList[pipeList.Count - 1].SetActive(true);
        return pipeList[pipeList.Count - 1];
    }

}
