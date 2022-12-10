using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONREADEDR : MonoBehaviour
{
    public TextAsset TextJSON;
    public TextAsset GameObjectSaved;
    [System.Serializable]

    public class Player
	{
        public string name;
        public int health;
        public int mana;
	}
    [System.Serializable]
    public class PlayerList
    {
        public List<Player> player;

    }
    public PlayerList myPlayerList = new PlayerList();


    [System.Serializable]
    public class CubeData
    {
        public string name;
        public Vector3 cubePosition;
        public Vector3 scale;
        public Vector3 rotation;
    }

    
    [System.Serializable]
    public class CubeDataList
    {
        public List<CubeData> cubes;
        public List<CubeData> spheres;

    }

    public CubeDataList myCubeDataList = new CubeDataList();


    public List<GameObject> ObjectList;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerList = JsonUtility.FromJson<PlayerList>(TextJSON.text);
        myCubeDataList = JsonUtility.FromJson<CubeDataList>(GameObjectSaved.text);
        Debug.Log(myPlayerList.player[0].name);
		//foreach (CubeData cubeData in myCubeDataList.cubes)
		//{
  //          GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
  //          cube.transform.position = cubeData.cubePosition;
  //          cube.name = cubeData.name;
  //      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
