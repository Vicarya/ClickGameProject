using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using Boo.Lang;
using System;


public class SaveScript : MonoBehaviour
{
    private static bool delete_flg = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            delete_flg = true;
        if (Input.GetKeyDown(KeyCode.N))
            delete_flg = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        delete();
    }

    public static Param load<Param>(string dir) where Param : class{
        Param t;
        var SavePath = Application.persistentDataPath + dir + ".bytes";

        // iOSでは下記設定を行わないとエラーになる
#if UNITY_IPHONE
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
        using(FileStream fs = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter bf = new BinaryFormatter();
            t = bf.Deserialize(fs) as Param;
        }
        return t;
    }

    public static void save<Param>(string dir, Param p) {
        var SavePath = Application.persistentDataPath + "/save.bytes";
        // iOSでは下記設定を行わないとエラーになる
#if UNITY_IPHONE
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif

        using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, p);
        }

    }

    public static void delete()
    {
        if (delete_flg)
        {
            PlayerPrefs.DeleteAll();
        }
    }

};