using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [System.Serializable]
    private class DataGemObject {
        public TypeColor type;
        public GameObject prefab;
    }

    [Tooltip("То из каких объектов будет спавнится дорога")]
    [SerializeField] private GameObject prefabRoad = null;

    [Tooltip("Длина генерируемой дороги")]
    [SerializeField] private int lenghtRoad = 50;

    [Tooltip("Растояние между дорогами")]
    [SerializeField] private Vector3 betweenRoads = new Vector3(1, 0, 0);

    [Header("Spawn on road")]
    [Tooltip("Количество линий на дороге, для спавна. !!! Эта переменная тесно связана с matrixGenObjsOnRoad, которая редактируется только в коде !!!")]
    [SerializeField] private int countlines = 5;

    [Tooltip("Шаг сдвига при переходе на другую линию. Нужен для спавна чего либо на дороге")]
    [SerializeField] private Vector3 offset = Vector3.zero;

    [Tooltip("Количество спавнов обьектов на дороге")]
    [SerializeField] private int countSpawnObjectsOnRoad = 2;

    [Tooltip("Сдвиг первого спавна обьектов на дороге")]
    [SerializeField] private Vector3 firstOffsetSpawnObjectsOnRoad = Vector3.zero;

    [Tooltip("Сдвиг для повторного спавна обьектов на дороге")]
    [SerializeField] private Vector3 offsetSpawnObjectsOnRoad = Vector3.zero;

    [Tooltip("Все что будет спавнится на дороге")]
    [SerializeField] private List<DataGemObject> objectsOnRoad = new List<DataGemObject> ();

    //Расположение камней на карте
    //0 - пусто
    //1 - красный, 2 - зеленный, 3 - синий
    private int[][,] matrixesGenObjsOnRoad = {
        new int[,] {
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 0, 1 }
        },
        new int[,] {
            { 2, 0, 1, 0, 3 },
            { 2, 0, 1, 0, 3 },
            { 2, 0, 1, 0, 3 },
            { 2, 0, 1, 0, 3 }
        },
        new int[,] {
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 }
        },
        new int[,] {
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 0, 1 },
            { 3, 0, 2, 1, 2 },
            { 3, 0, 1, 0, 2 },
            { 3, 1, 3, 0, 2 },
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 },
            { 1, 0, 3, 0, 2 }
        }
    };


    private List<GameObject> allObjects = new List<GameObject>();

    private new Transform transform = null;

    private void Start() {
        transform = GetComponent<Transform>();

        //Создание дороги
        for (int i = 0; i < lenghtRoad; ++i) {
            SpawnRoad(i);
        }

        //Генерация объектов на дороге
        GenerateObjectsOnRoad();
    }

    private void GenerateObjectsOnRoad() {

        Vector3 posObj = firstOffsetSpawnObjectsOnRoad;

        for (int i = 0; i < countSpawnObjectsOnRoad; ++i) {

            int[,] randMatrix = matrixesGenObjsOnRoad[Random.Range(0, matrixesGenObjsOnRoad.Length)];

            if (i != 0) posObj += offsetSpawnObjectsOnRoad;

            for (int z = 0; z < randMatrix.GetLength(0); ++z) {

                if (z != 0) posObj.z += offset.z;

                for (int x = 0; x < Mathf.Min(countlines, randMatrix.GetLength(1)); ++x) {
                    int typeSpawn = randMatrix[z, x];
                    if (typeSpawn == 0) continue;

                    int offsetX = x - (int)((countlines - 1) * 0.5F);

                    Vector3 curPosObj = new Vector3(posObj.x + (offset.x * offsetX), posObj.y + offset.y, posObj.z);

                    TypeColor type = (TypeColor)(randMatrix[z, x] - 1);

                    //Spawn
                    GameObject obj = Instantiate(GetPrefabByType(type));

                    Transform transformObj = obj.transform;
                    transformObj.SetParent(transform);

                    transformObj.localPosition = curPosObj;

                    allObjects.Add(obj);

                }
            }
        }

    }

    private GameObject GetPrefabByType(TypeColor type) {
        foreach(DataGemObject data in objectsOnRoad) {
            if (data.type == type) return data.prefab;
        }

        return null;
    }

    private void SpawnRoad(int i) {
        GameObject road = Instantiate(prefabRoad);

        Transform transformRoad = road.transform;
        transformRoad.SetParent(transform);

        transformRoad.localPosition = betweenRoads * i;

        allObjects.Add(road);
    }
}
