using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public class TwitchGetData : MonoBehaviour
{
    private static HttpClientLib hcl;
    void Awake()
    {
        
    }

    public static async UniTask<string> GetDisplayName(string _user_id)
    {
        string url = @$"https://api.twitch.tv/helix/users?login={_user_id}";
        var client = HttpClientLib.client;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "f6iujnylw8qeewspjadbdda21sd2em");
        client.DefaultRequestHeaders.Add("Client-Id","r781obelza1okxmhicnvmw48fshhe0");
        var response = await client.GetAsync(url);
        if(response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JsonConvert.DeserializeObject(responseBody);
            string value = jsonObject.data[0].display_name;
            return value;
        }
        else
        {
            return "Did not get display name";
        }
    }
    
}
