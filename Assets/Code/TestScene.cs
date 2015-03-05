using UnityEngine;
using System.Collections;

public class TestScene : MonoBehaviour
{
	public void DebugLog()
	{
		Debug.Log("This is a log message.");
	}

	public void DebugWarning()
	{
		Debug.LogWarning("This is a warning message.");
	}

	public void DebugError()
	{
		Debug.LogError("This is an error message.");
	}
}
