using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveAndLoad{
    public static void SaveData(Data data){
        FileStream stream = new FileStream(Application.persistentDataPath + "/saveData.data", FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data LoadData(){
        if(File.Exists(Application.persistentDataPath + "/saveData.data")){
            FileStream stream = new FileStream(Application.persistentDataPath + "/saveData.data", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            
            return data;
        }else{
            return null;
        }
    }
}
