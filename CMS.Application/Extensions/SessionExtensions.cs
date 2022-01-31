using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CMS.Application.Extensions
{
    // Bu iki static methodlar sepet işlemlerinde kullanılacak. Sepete atmak istediğimizde SetJson() çalışacak ve Product tipindeki ürünü Json tipine dönüştürecek yani Serialize edecektir. Sepetten yani Sessiondan bu ürünü alıp uygulama tarafında işleme sokacağımda ise Product tipine dönüştüreceğim ki uygulama anlasın bunuda GetJson() fonk. ile yapacağım
    public static class SessionExtensions
    {
        // Sepette gönderilen ürünler Session üzerince saklanacaktır. Session browser' da geçici olarak bizim ömrünü belirleyeceğimiz bir depolama alanı olarak düşünebiliriz.
        public static void SetJson(this ISession session, string key, object value)
        {
            // SerializeObject fonk. ile Product tipindeki ürünü Json tipine serialize ediyoruz.
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            // sessionData null ise bize default olarak methoda gelen tipi, değilse Json tipindeki ürünü deserialize ederek product tipine dönüştürecektir.
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
