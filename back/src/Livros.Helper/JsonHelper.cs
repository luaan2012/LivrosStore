// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using System;
// using System.Collections.Generic;
// using System.Text;

namespace TudoFarmaRep.Web
{
    public static class JsonHelper {
        // public static string GetJson(object instance) {
        //     var json = string.Empty;
        //     try {
        //         json = JsonConvert.SerializeObject(instance);
        //     } catch (Exception e){
        //         var teste = e;
        //     }
        //     return json;
        // }
        // public static T GetObject<T>(string json) where T : new() {
        //     T instance = default(T);
        //     try {
        //         instance = JsonConvert.DeserializeObject<T>(json);
        //     }catch(Exception e) {
        //         var teste = e;
        //     }
        //     return instance;
        // }
        // public static T GetObject<T>(string json, string mainTag) where T : new() {
        //     T instance = default(T);
        //     try {
        //         instance = JToken.Parse(json)[mainTag].ToObject<T[]>()[0];
        //     } catch(Exception e) {
        //         var teste = e;
        //     }
        //     return instance;
        // }
        // public static T[] GetObjectArray<T>(string json, string mainTag) where T : new() {
        //     T[] instance = null;
        //     try {
        //         instance = JToken.Parse(json)[mainTag].ToObject<T[]>();
        //     } catch(Exception e) {
        //         var teste = e;
        //     }
        //     return instance;
        // }
    }
}
