using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public class ObjectPooling : MonoBehaviour
    {

        public GameObject poolObject;
        public string objectName;

        [Range(1, 10000000)]
        public int numberOfPooledObjects = 10;

        [Range(1, 10000000)]
        public int regenerateMoreObjs = 5;

        public Vector3 spawnLocation = new Vector3(0, -100, 0);
        private Stack<GameObject> collectionOfObjs;
        private GameObject poolGroup;

        void Awake()
        {
            collectionOfObjs = new Stack<GameObject>();
            poolGroup = new GameObject();
            poolGroup.name = objectName + " Pool";
            poolGroup.transform.parent = transform;
            GenerateCollection(numberOfPooledObjects);
        }

        public void ReturnObject(GameObject returnObj)
        {
            returnObj.transform.position = spawnLocation;
            returnObj.SetActive(false);
            collectionOfObjs.Push(returnObj);
        }

        public GameObject GetNextObject()
        {
            GameObject nextObj;
            if (collectionOfObjs.Count != 0)
            {
                nextObj = collectionOfObjs.Pop();
                nextObj.SetActive(true);
                return nextObj;
            }
            else
            {
                GenerateCollection(regenerateMoreObjs);
            }

            if (collectionOfObjs.Count != 0)
            {
                nextObj = collectionOfObjs.Pop();
                nextObj.SetActive(true);
                return nextObj;
            }
            else
            {
                print("Error: Cannot get another GameObject");
                return null;
            }
        }

        private void GenerateCollection(int length)
        {
            GameObject objectRef;
            for (int i = 0; i < length; i++)
            {
                objectRef = Instantiate(poolObject, Vector3.zero, Quaternion.identity) as GameObject;
                objectRef.name = objectName;
                objectRef.transform.position = spawnLocation;
                objectRef.transform.parent = poolGroup.transform;
                objectRef.SetActive(false);
                collectionOfObjs.Push(objectRef);
            }
        }
    }
}
