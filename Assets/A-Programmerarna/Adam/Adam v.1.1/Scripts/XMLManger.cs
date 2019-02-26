using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class XMLManger : Singleton<XMLManger>
{
	void Awake(){
        if (!Directory.Exists(Application.dataPath + "/xml/")) {
			Directory.CreateDirectory(Application.dataPath + "/xml/");
		}
	}

	public void Savequests(List<Quest> quests, string saveName){
		XmlSerializer serializer = new XmlSerializer(typeof(List<Quest>));
		FileStream stream = new FileStream (Application.dataPath + "/xml/" + saveName + ".xml", FileMode.Create);

		serializer.Serialize (stream, quests);
		stream.Close ();
	}

	public List<Quest> Loadquests(string saveName){
		List<Quest> temp;
		XmlSerializer serializer = new XmlSerializer(typeof(List<Quest>));
		FileStream stream = new FileStream (Application.dataPath + "/xml/" + saveName + ".xml", FileMode.Open);
		temp = serializer.Deserialize (stream) as List<Quest>;

		stream.Close ();
		return temp;
	}

    public void SaveString(string s, string saveName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(string));
        FileStream stream = new FileStream(Application.dataPath + "/xml/" + saveName + ".xml", FileMode.Create);

        serializer.Serialize(stream, s);
        stream.Close();
    }

    public string LoadString(string saveName)
    {
        string temp;
        XmlSerializer serializer = new XmlSerializer(typeof(string));
        FileStream stream = new FileStream(Application.dataPath + "/xml/" + saveName + ".xml", FileMode.Open);
        temp = serializer.Deserialize(stream) as string;

        stream.Close();
        return temp;
    }
}