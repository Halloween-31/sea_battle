using Newtonsoft.Json;

namespace asp_MVC_letsTry.SessionExtensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var obj = JsonConvert.SerializeObject(value);
            session.SetString(key, obj);

            //var obj = JsonSerializer.Serialize<T>(value);
            //var obj2 = JsonSerializer.Create();
            //var obj3 = obj2.Serialize()
            //var obj2 = JsonConvert.SerializeObject(value);           
            //session.SetString(key, value.ToString());            



            /*if (value is Game_duel)
            {
                var firstF = JsonConvert.SerializeObject((value as Game_duel).first_battle_field);                
                var secondF = JsonConvert.SerializeObject((value as Game_duel).second_battle_field);
                string[] arr = new string[] { firstF, secondF };
                var res = JsonConvert.SerializeObject(arr);
                session.SetString(key, res);
            }*/
        }
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value is null ? default(T) : JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });

            //return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);            





            /*if(value == null)
            {
                return default(T);
            }
            else
            {                
                //return JsonConvert.DeserializeObject<T>(value);
                string[] arr = JsonConvert.DeserializeObject<string[]>(value);
                var firstF = JsonConvert.DeserializeObject<battle_field>(arr[0]);
                var secondF = JsonConvert.DeserializeObject<battle_field>(arr[1]);
                var res = new Game_duel() { first_battle_field = firstF, second_battle_field = secondF };
                if(res is T)
                {
                    return res;
                }
                return null;
            }*/
        }
    }
}