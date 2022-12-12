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
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public bool rigidBody;
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

        //string path = "Assets/Scenes/JSONCubes.txt";
        //string path = "Assets/Scenes/example.level";
        //string path = "Assets/Scenes/JSONCubesTest.txt";
       // string path = "Assets/Scenes/SavingToThisFile.txt";
        string path = "Assets/Scenes/test.txt";
        string json;
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        json = reader.ReadToEnd();

        Debug.Log(reader.ReadToEnd());

		dataList = JsonUtility.FromJson<DataList>(json);

		Debug.Log(dataList.cubes[0].name);
		Debug.Log(dataList.cubes[0]);
		foreach (Data cubeData in dataList.cubes)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = cubeData.position;
			cube.name = cubeData.name;
			cube.transform.localScale = cubeData.scale;
			cube.transform.eulerAngles = new Vector3(
					  cubeData.rotation.x,
					  cubeData.rotation.y,
					  cubeData.rotation.z
					  );
			cube.tag = "cube";
			if (cubeData.rigidBody == true)//put info here abt it having rigidbody
			{
				cube.AddComponent<Rigidbody>();
			}
		}
		foreach (Data sphereData in dataList.spheres)
		{
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = sphereData.position;
			sphere.name = sphereData.name;
			sphere.transform.localScale = sphereData.scale;
			sphere.transform.eulerAngles = new Vector3(
				sphereData.rotation.x,
				sphereData.rotation.y,
				sphereData.rotation.z
				);
			sphere.tag = "sphere";
			if (sphereData.rigidBody == true)//put info here abt it having rigidbody
			{
				sphere.AddComponent<Rigidbody>();
			}
		}
		foreach (Data capsuleData in dataList.capsules)
		{
			GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			capsule.transform.position = capsuleData.position;
			capsule.name = capsuleData.name;
			capsule.transform.localScale = capsuleData.scale;
			capsule.transform.eulerAngles = new Vector3(
				capsuleData.rotation.x,
				capsuleData.rotation.y,
				capsuleData.rotation.z
				);
			capsule.tag = "capsule";
            if (capsuleData.rigidBody == true)//put info here abt it having rigidbody
            {
            }
            capsule.AddComponent<Rigidbody>();

        }
        foreach (Data planeData in dataList.planes)
		{
			GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			plane.transform.position = planeData.position;
			plane.name = planeData.name;
			plane.transform.localScale = planeData.scale;
			plane.transform.eulerAngles = new Vector3(
				planeData.rotation.x,
				planeData.rotation.y,
				planeData.rotation.z
				);
			plane.tag = "plane";
            if (planeData.rigidBody == true)//put info here abt it having rigidbody
            {
                plane.AddComponent<Rigidbody>();
            }
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
            data.position = arrayofcubes[i].transform.position;
            data.scale = arrayofcubes[i].transform.localScale;
            data.rotation = arrayofcubes[i].transform.eulerAngles;
            data.rigidBody = true;
            cubeData.Add(data);
		}
        for (int i = 0; i < arrayofspheres.Length; i++)
        {
            Data data = new Data();

            data.name = arrayofspheres[i].name;
            data.position = arrayofspheres[i].transform.position;
            data.scale = arrayofspheres[i].transform.localScale;
            data.rotation = arrayofspheres[i].transform.eulerAngles;

            sphereData.Add(data);
        }
        for (int i = 0; i < arrayofcapsules.Length; i++)
        {
            Data data = new Data();

            data.name = arrayofcapsules[i].name;
            data.position = arrayofcapsules[i].transform.position;
            data.scale = arrayofcapsules[i].transform.localScale;
            data.rotation = arrayofcapsules[i].transform.eulerAngles;
            CapsuleData.Add(data);
        }
        for (int i = 0; i < arrayofplanes.Length; i++)
        {
            Data data = new Data();

            data.name = arrayofplanes[i].name;
            data.position = arrayofplanes[i].transform.position;
            data.scale = arrayofplanes[i].transform.localScale;
            data.rotation = arrayofplanes[i].transform.eulerAngles;
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
		string path = "Assets/Scenes/example.level";
		FileStream fileStream = new FileStream(path, FileMode.Create);

		using (StreamWriter writer = new StreamWriter(fileStream))
		{
			writer.Write(json);
		}
	}
}