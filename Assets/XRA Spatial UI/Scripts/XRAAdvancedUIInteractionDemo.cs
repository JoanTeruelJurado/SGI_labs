using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRAAdvancedUIInteractionDemo : MonoBehaviour
{
    [SerializeField] private PrimitiveType[] primitiveTypes = { PrimitiveType.Cube, PrimitiveType.Sphere, PrimitiveType.Cylinder, PrimitiveType.Capsule };

    [SerializeField] private Vector3 spawnPosition = new Vector3(1, 1.5f, 2);
    [SerializeField] private Vector3 spawnRotation = new Vector3(45, 45, 0);
    [SerializeField] private Vector3 spawnScale = new Vector3(0.35f, 0.35f, 0.35f);
    private List<GameObject> objs = new List<GameObject>();
    private int currentIndex = 0;
    public void Start()
    {
        GenerateObjs();
    }
    public void GenerateObjs()
    {
        GameObject container = new GameObject("ObjectsContainer");
        for (int i = 0; i < primitiveTypes.Length; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(primitiveTypes[i]);
            obj.transform.parent = container.transform;
            obj.transform.position = spawnPosition;
            obj.transform.rotation = Quaternion.Euler(spawnRotation);
            obj.transform.localScale = spawnScale;
            obj.SetActive(false);
            objs.Add(obj);
        }
    }
    public void DisplayObj(bool state)
    {
        if (state)
        {
            objs[currentIndex].SetActive(false);
            currentIndex = (currentIndex + 1) % objs.Count;
            objs[currentIndex].SetActive(true);
        }
        else
        {
            objs[currentIndex].SetActive(false);
        }
    }
}
