using System.Collections;
using System.Collections.Generic;
using CandyCoded.env;
using Cysharp.Threading.Tasks;
using System.Net.Http;
using System.Text;
using UnityEngine;

public class ReplyGenerateManager : MonoBehaviour
{
    public static ReplyGenerateManager instance;
    private static ReplyGenerateData replyGeneraData;
    private string rgDataStr;
    private static string MIIBO_URL;
    private static string MIIBO_API_KEY;
    private static string MIIBO_AGENT_ID;

    public void Initialize()
    {
        replyGeneraData = new ReplyGenerateData();
        env.TryParseEnvironmentVariable("MIIBO_URL", out MIIBO_URL);
        env.TryParseEnvironmentVariable("MIIBO_API_KEY", out MIIBO_API_KEY);
        env.TryParseEnvironmentVariable("MIIBO_AGENT_ID", out MIIBO_AGENT_ID);
    }

    //httpで送るためのデータに変換
    public void SetReplyGenerateData(string res)
    {
        ReplyDataOverWrite(res);
        rgDataStr = JsonUtility.ToJson(replyGeneraData).ToString();
    }
    // データを変換して上書き
    private void ReplyDataOverWrite(string res)
    {
        replyGeneraData.api_key = MIIBO_API_KEY;
        replyGeneraData.agent_id = MIIBO_AGENT_ID;
        replyGeneraData.utterance = res;
    }
    // MiiboのAPI呼び出し返答を生成
    public async UniTask<string> GenerateReply()
    {
        string apiURL = MIIBO_URL;
        var client = HttpClientLib.client;
        var content = new StringContent(rgDataStr, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(apiURL, content);
        if(response.IsSuccessStatusCode)
        {
            Debug.Log("Connected Miibo");
            var res = await response.Content.ReadAsStringAsync();
            ReplyData replyData = JsonUtility.FromJson<ReplyData>(res);
            string value = replyData.bestResponse.utterance;
            return value;
        }
        else
        {
            Debug.Log(response.StatusCode);
            return null;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}