using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class CSV
{
	static CSV csv;
	public List<List<string>> dataList;
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
		dataList = new List<List<string>>();
	}

	public List<List<string>> loadFile(string path, string fileName)
	{
		dataList.Clear();
		StreamReader sr = null;
		try
		{
			sr = File.OpenText(path + "//" + fileName);
		}
		catch
		{
			Debug.Log ("没有找到文件");
			return new List<List<string>>();
		}

		string line;

		while((line = sr.ReadLine()) != null)
		{
			List<string> newList = new List<string> (line.Split (','));

			dataList.Add(newList);
		}

		sr.Close();
		sr.Dispose();

		return dataList;
	}
}
