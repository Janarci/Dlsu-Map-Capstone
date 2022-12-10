using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

	public int experience;
	public int level
	{
		get { return experience / 750; }
	}
}
