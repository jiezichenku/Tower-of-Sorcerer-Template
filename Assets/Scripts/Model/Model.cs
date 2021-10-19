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

    private static JObject Readjson(string fileName)
    {
        string jsonFile = folderPath + fileName + ".json";//JSONÎÄ¼þÂ·¾¶
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

