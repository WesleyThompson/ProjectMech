using UnityEngine;
using System.Collections;

public class SearchObjectPool : MonoBehaviour {

	public static ObjectPooling GetObject(string objName)
    {
        ObjectPooling[] list = GameObject.Find(GlobalVariables.ObjectPooling).GetComponents<ObjectPooling>();
        for(int i = 0; i<list.Length; i++)
        {
            if(list[i].objectName == objName)
            {
                return list[i];
            }
        }
        print("Error: Did not find \"" + objName + "\"");
        return null;
    }
}
