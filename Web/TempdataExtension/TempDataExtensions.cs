using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Service.Entities;

namespace Web.TempdataExtension
{
    public static class TempDataExtensions
    {
        //hoac setting T type class ==> tuy theo muc dich Business
        //Su dung ITempDataDictionary de config put , get type gi cho thang TempData
        public static void PutListOrder<T>(this ITempDataDictionary tempData, string key, T value) where T : List<Order>
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T GetListOrder<T>(this ITempDataDictionary tempData, string key) where T : List<Order>
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static void PutString<T>(this ITempDataDictionary tempData, string key, T value) where T  : IEquatable<string>
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T GetString<T>(this ITempDataDictionary tempData, string key) where T : IComparable<string>
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? (T) (object) String.Empty : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
