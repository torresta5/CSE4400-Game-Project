using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveGame(StateNameController stateNameController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/state.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        StateNameController data = new StateNameController();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StateNameController LoadData ()
    {
        string path = Application.persistentDataPath + "/state.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StateNameController data = formatter.Deserialize(stream) as StateNameController;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
