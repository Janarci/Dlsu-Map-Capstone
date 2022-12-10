using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MenuItems
{
    [System.Serializable]
    public class Data
    {
        public string name;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
    }


    [System.Serializable]
    public class DataList
    {
        public List<Data> cubes;
        public List<Data> spheres;
        public List<Data> capsules;
        public List<Data> planes;

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
        DataList dataList = new DataList();

        string path = "Assets/Scenes/JSONCubes.txt";
        //string path = "Assets/Scenes/JSONCubesTest.txt";
        string json;
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        json = reader.ReadToEnd();

        Debug.Log(reader.ReadToEnd());

        dataList = JsonUtility.FromJson<DataList>(json);

        Debug.Log(dataList.cubes[0].name);
		foreach (Data cubeData in dataList.cubes)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = cubeData.Position;
			cube.name = cubeData.name;
			cube.transform.localScale = cubeData.Scale;
			cube.transform.eulerAngles = new Vector3(
                cubeData.Rotation.x,
                cubeData.Rotation.y,
                cubeData.Rotation.z
                );
            cube.tag = "cube";
        }
        foreach (Data sphereData in dataList.spheres)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = sphereData.Position;
            sphere.name = sphereData.name;
            sphere.transform.localScale = sphereData.Scale;
            sphere.transform.eulerAngles = new Vector3(
                sphereData.Rotation.x,
                sphereData.Rotation.y,
                sphereData.Rotation.z
                );
            sphere.tag = "sphere";
        }
        foreach (Data capsuleData in dataList.capsules)
        {
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.transform.position = capsuleData.Position;
            capsule.name = capsuleData.name;
            capsule.transform.localScale = capsuleData.Scale;
            capsule.transform.eulerAngles = new Vector3(
                capsuleData.Rotation.x,
                capsuleData.Rotation.y,
                capsuleData.Rotation.z
                );
            capsule.tag = "capsule";

        }
        foreach (Data planeData in dataList.planes)
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = planeData.Position;
            plane.name = planeData.name;
            plane.transform.localScale = planeData.Scale;
            plane.transform.eulerAngles = new Vector3(
                planeData.Rotation.x,
                planeData.Rotation.y,
                planeData.Rotation.z
                );
            plane.tag = "plane";

        }



        reader.Close();
    }
    [MenuItem("Load level/Save File")]
    static void WriteToFile()
	{
        DataList myCubeDataList2 = new DataList();

        GameObject[] arrayofcubes = GameObject.FindGameObjectsWithTag("cube");
        GameObject[] arrayofspheres = GameObject.FindGameObjectsWithTag("sphere");
        GameObject[] arrayofcapsules = GameObject.FindGameObjectsWithTag("capsule");
        GameObject[] arrayofplanes = GameObject.FindGameObjectsWithTag("plane");

        List<Data> cubeData = new List<Data>();
        List<Data> sphereData = new List<Data>();
        List<Data> CapsuleData = new List<Data>();
        List<Data> planeData = new List<Data>();


		for (int i = 0; i < arrayofcubes.Length; i++)
		{
            Data data = new Data();

            data.name = arrayofcubes[i].name;
            data.Position = arrayofcubes[i].transform.position;
            data.Scale = arrayofcubes[i].transform.localScale;
            data.Rotation = arrayofcubes[i].transform.eulerAngles;
            cubeData.Add(data);
		}
        for (int i = 0; i < arrayofspheres.Length; i++)
        {
            Data data = new Data();

            data.name = arrayofspheres[i].name;
            data.Position = arrayofspheres[i].transform.position;
            data.Scale = arrayofspheres[i].transform.localScale;
            data.Rotation = arrayofspheres[i].transform.eulerAngles;

            sphereData.Add(data);
        }
        for (int i = 0; i < arrayofcapsules.Length; i++)
        {
            Data data = new Data();

            data.name = arrayofcapsules[i].name;
            data.Position = arrayofcapsules[i].transform.position;
            data.Scale = arrayofcapsules[i].transform.localScale;
            data.Rotation = arrayofcapsules[i].transform.eulerAngles;
            CapsuleData.Add(data);
        }
        for (int i = 0; i < arrayofplanes.Length; i++)
        {
            Data data = new Data();

            data.name = arrayofplanes[i].name;
            data.Position = arrayofplanes[i].transform.position;
            data.Scale = arrayofplanes[i].transform.localScale;
            data.Rotation = arrayofplanes[i].transform.eulerAngles;
            planeData.Add(data);
        }

        //CubeData data = new CubeData();

        //data.name = arrayofcubes[0].name;
        //data.cubePosition = arrayofcubes[0].transform.position;
        //data.Scale = arrayofcubes[0].transform.localScale;
        //cubeData.Add(data);

        //Debug.Log(data.name);

        myCubeDataList2.cubes = cubeData;
        myCubeDataList2.spheres = sphereData;
        myCubeDataList2.capsules = CapsuleData;
        myCubeDataList2.planes = planeData;





		string json = JsonUtility.ToJson(myCubeDataList2);
		string path = "Assets/Scenes/JSONCubesTest.txt";
		FileStream fileStream = new FileStream(path, FileMode.Create);

		using (StreamWriter writer = new StreamWriter(fileStream))
		{
			writer.Write(json);
		}
	}
}