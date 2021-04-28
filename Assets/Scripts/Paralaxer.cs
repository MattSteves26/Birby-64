using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxer : MonoBehaviour
{
    class PoolObject {
        public Transform transform;
        public bool inUse;
        public PoolObject(Transform t) { transform = t;}
        public void Use() { inUse = true;}
        public void Dispose() { inUse = false;}
    }

    [System.Serializable]
    public struct YSpawnRange {
        public float min;
        public float max;
    }
    [System.Serializable]
    public struct YRotRange {
        public float min;
        public float max;
    }

    public GameObject Prefab;
    public int poolSize;
    public float shiftSpeed;
    public float spawnRate;

    public YSpawnRange ySpawnRange;
    public YRotRange yRotRange;
    public Vector3 defaultSpawnPos;
    public bool spawnImmediate;
    public Vector3 immediateSpawnPos;
    public Vector2 targetAspectRatio;

    float spawnTimer;
    float targetAspect;
    PoolObject[] poolObjects;
    FB_GameManager game;

    void Awake() {
        Configure();
    }

    void Start() {
        game = FB_GameManager.Instance;
    }

    void OnEnable(){
        FB_GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable(){
        FB_GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;

    }

    void OnGameOverConfirmed(){
        for(int i = 0; i< poolObjects.Length; i++){
            poolObjects[i].Dispose();
            poolObjects[i].transform.position = Vector3.one * 1000;
        }
        if(spawnImmediate){
            SpawnImmediate();
        }
    }

    void Update(){
        if (game.GameOver) return;

        Shift();
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnRate){
            Spawn();
            spawnTimer = 0;
        }

    }

    void Configure(){
        targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        poolObjects = new PoolObject[poolSize];
        for(int i = 0 ; i < poolObjects.Length; i++){
            GameObject go = Instantiate(Prefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);
            t.position = Vector3.one * 1000;
            poolObjects[i] = new PoolObject(t);
        }

        if(spawnImmediate){
            SpawnImmediate();
        }
    }

    void Spawn(){
        Transform t = GetPoolObject();
        if(t == null) return; //pool size is too small
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.Euler(0,Random.Range(yRotRange.min, yRotRange.max), 0);
        t.rotation = rot;
        pos.x = (defaultSpawnPos.x * Camera.main.aspect) / targetAspect;
        Debug.Log("Position x" + pos.x);
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;
    }

    void SpawnImmediate(){
        Transform t = GetPoolObject();
        if(t == null) {
            Debug.Log("pool size too small");
            return; 
        }
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.Euler(0,Random.Range(yRotRange.min, yRotRange.max), 0);
        t.rotation = rot;
        pos.x = (immediateSpawnPos.x * Camera.main.aspect) / targetAspect;
        Debug.Log("SIPosition x" + pos.x);
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;
        Spawn();
    }

    void Shift(){
        for(int i = 0 ; i < poolObjects.Length ; i++){
            poolObjects[i].transform.position += -Vector3.right * shiftSpeed * Time.deltaTime;
            CheckDisposeObject(poolObjects[i]);
        }
    }

    void CheckDisposeObject(PoolObject poolObject){
        if(poolObject.transform.position.x < (-defaultSpawnPos.x * Camera.main.aspect) / targetAspect){
            poolObject.Dispose();
            poolObject.transform.position = Vector3.one * 1000;
        }
    }

    Transform GetPoolObject() {
        for(int i = 0 ; i < poolObjects.Length ; i++){
            if(!poolObjects[i].inUse) {
                poolObjects[i].Use();
                return poolObjects[i].transform;
            }
        }
        return null;
    }
}
