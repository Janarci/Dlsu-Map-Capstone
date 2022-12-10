using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MenuItems
{
    [System.Serializable]
    public class CubeData
    {
        public string name;
        public Vector3 cubePosition;
        public Vector3 Rotation;
        public Vector3 Scale;
    }


    [System.Serializable]
    public class CubeDataList
    {
        public List<CubeData> cubes;
        public List<CubeData> spheres;
        public List<CubeData> capsules;
        public List<CubeData> planes;

    }


    [MenuItem("Load level/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        GameObject cube =  GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "test spawn";
        //Assets/Scenes/JSONTEST.txt
    }

    [MenuItem("Load level/Read file")]
    static void ReadString()
    {
        CubeDataList myCubeDataList = new CubeDataList();

        string path = "Assets/Scenes/JSONCubes.txt";
        //string path = "Assets/Scenes/JSONCubesTest.txt";
        string json;
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        json = reader.ReadToEnd();

        Debug.Log(reader.ReadToEnd());

        myCubeDataList = JsonUtility.FromJson<CubeDataList>(json);

        Debug.Log(myCubeDataList.cubes[0].name);
		foreach (CubeData cubeData in myCubeDataList.cubes)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = cubeData.cubePosition;
			cube.name = cubeData.name;
			cube.transform.localScale = cubeData.Scale;
			cube.transform.eulerAngles = new Vector3(
                cubeData.Rotation.x,
                cubeData.Rotation.y + 180,
                cubeData.Rotation.z
                );
            cube.tag = "cube";
        }
        foreach (CubeData sphereData in myCubeDataList.spheres)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = sphereData.cubePosition;
            sphere.name = sphereData.name;
            sphere.transform.localScale = sphereData.Scale;
            sphere.transform.eulerAngles = new Vector3(
                sphereData.Rotation.x,
                sphereData.Rotation.y + 180,
                sphereData.Rotation.z
                );
        }
        foreach (CubeData capsuleData in myCubeDataList.capsules)
        {
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.transform.position = capsuleData.cubePosition;
            capsule.name = capsuleData.name;
            capsule.transform.localScale = capsuleData.Scale;
            capsule.transform.eulerAngles = new Vector3(
                capsuleData.Rotation.x,
                capsuleData.Rotation.y + 180,
                capsuleData.Rotation.z
                );
        }
        foreach (CubeData planeData in myCubeDataList.planes)
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = planeData.cubePosition;
            plane.name = planeData.name;
            plane.transform.localScale = planeData.Scale;
            plane.transform.eulerAngles = new Vector3(
                planeData.Rotation.x,
                planeData.Rotation.y + 180,
                planeData.Rotation.z
                );
        }



        reader.Close();
    }
    [MenuItem("Load level/Save File")]
    static void WriteToFile()
	{
        CubeDataList myCubeDataList2 = new CubeDataList();

        GameObject[] arrayofcubes = GameObject.FindGameObjectsWithTag("cube");

        List<CubeData> cubeData = new List<CubeData>();

        CubeData data = new CubeData();

        data.name = arrayofcubes[0].name;
        data.cubePosition = arrayofcubes[0].transform.position;
        data.Scale = arrayofcubes[0].transform.localScale;
        cubeData.Add(data);

        Debug.Log(data.name);

        myCubeDataList2.cubes = cubeData;





		string json = JsonUtility.ToJson(myCubeDataList2);
		string path = "Assets/Scenes/JSONCubesTest.txt";
		FileStream fileStream = new FileStream(path, FileMode.Create);

		using (StreamWriter writer = new StreamWriter(fileStream))
		{
			writer.Write(json);
		}
	}
}