using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class CSV
{
	static CSV csv;
	public List<string> dataList;
	public static CSV Instance
	{
		get
		{
			if (csv == null)
			{
				csv = new CSV();
			}

			return csv;
		}
	}

	private CSV()
	{
		dataList = new List<string>();
	}

	public List<string> loadFile(string path, string fileName)
	{
		dataList.Clear();
		StreamReader sr = null;
		try
		{
			sr = File.OpenText(path + "//" + fileName);
			Debug.Log("找到文件");
		}
		catch
		{
			Debug.Log ("没有找到文件");
			return new List<string>();
		}

		string line;

		while((line = sr.ReadLine()) != null)
		{
			dataList.Add(line);
		}

		sr.Close();
		sr.Dispose();

		return dataList;
	}
}
