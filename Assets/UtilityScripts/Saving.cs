using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Saving : MonoBehaviour
{
	public DataApplication[] data;

	public Text visualDisplayIndex;

	int currentIndex = 0;
	int amount = 0;

	void Awake ()
	{
		LoadMonster (0);
	}
	
	void Update ()
	{
		visualDisplayIndex.text = "Index: " + (currentIndex + 1) + "/" + amount;
	}

	void OnApplicationQuit ()
	{
		SaveMonster ();

	}

	public void SaveMonster ()
	{
		MonsterXMLSerializer xml = new MonsterXMLSerializer ();
		if (File.Exists (Path.Combine (Application.dataPath, "monsters.xml")))
			xml.monsters = MonsterXMLSerializer.Load (Path.Combine (Application.dataPath, "monsters.xml")).monsters;
		if (currentIndex > xml.monsters.Count - 1) 
			xml.monsters.Add (new Monster ());
		else 
			for (int i = 0; i < data.Length; i++) {
				data [i].SaveChanges (xml.monsters [currentIndex]);
			}

		xml.Save (Path.Combine (Application.dataPath, "monsters.xml"));
		LoadMonster (currentIndex);
	}

	public void LoadMonsterRight ()
	{
		LoadMonster (++currentIndex);
		SaveMonster ();
	}

	public void LoadMonsterLeft ()
	{
		if (currentIndex - 1 > -1) {
			LoadMonster (--currentIndex);
		}
	}

	public void LoadMonster (int index)
	{
		MonsterXMLSerializer xml = new MonsterXMLSerializer ();

		if (File.Exists (Path.Combine (Application.dataPath, "monsters.xml")))
			xml = MonsterXMLSerializer.Load (Path.Combine (Application.dataPath, "monsters.xml"));
		else {
			SaveMonster ();
			xml = MonsterXMLSerializer.Load (Path.Combine (Application.dataPath, "monsters.xml"));
		}

		if (index > xml.monsters.Count - 1) {
			SaveMonster ();
			xml = MonsterXMLSerializer.Load (Path.Combine (Application.dataPath, "monsters.xml"));
		}

		for (int i = 0; i < data.Length; i++) {
			data [i].LoadChanges (xml.monsters [index]);
		}
		amount = xml.monsters.Count;
	}

	public void NewMonster ()
	{
		LoadMonster (++currentIndex);
		SaveMonster ();
	}

	public void RemoveMonster ()
	{
		if (File.Exists (Path.Combine (Application.dataPath, "monsters.xml"))) {
			MonsterXMLSerializer xml = MonsterXMLSerializer.Load (Path.Combine (Application.dataPath, "monsters.xml"));

			xml.monsters.RemoveAt (currentIndex);
		
			if (currentIndex > xml.monsters.Count - 1)
				currentIndex--;

			if (currentIndex < 0) {
				currentIndex = 0;
				xml.monsters.Add (new Monster ());
			}
			if (amount > 1) 
				amount--;

			xml.Save (Path.Combine (Application.dataPath, "monsters.xml"));
			LoadMonster (currentIndex);
		}
	}
}