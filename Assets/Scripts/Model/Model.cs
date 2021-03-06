using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class Model
{
    private static string folderPath = @"Assets\Scripts\Model\Data\";
    public static JObject GetInitBraverData()
    {
        JObject initStatus = Readjson("InitBraverStatus");
        return initStatus;
    }

    public static JObject GetEnemyData(int enemyID)
    {
        string jsonFile = $@"EnemyData\Enemy{enemyID:D4}";
        JObject enemyData = Readjson(jsonFile);
        return enemyData;
    }

    public static JObject GetItemData()
    {
        string jsonFile = $@"ItemData\ItemInfo";
        JObject itemData = Readjson(jsonFile);
        return itemData;
    }
    private static JObject Readjson(string fileName)
    {
        string jsonFile = folderPath + fileName + ".json";//JSON?ļ?·??
        using (System.IO.StreamReader f = System.IO.File.OpenText(jsonFile))
        {
            using (JsonTextReader reader = new JsonTextReader(f))
            {
                JObject o = (JObject)JToken.ReadFrom(reader);
                return o;
            }
        }
    }
}

