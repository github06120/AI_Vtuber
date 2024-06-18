using Cysharp.Threading.Tasks;
using Python.Runtime;
using UnityEngine;
using CandyCoded.env;
using System.Security.Policy;
using Live2D.Cubism.Framework.Json;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks.Triggers;


public class AituberMain : MonoBehaviour
{
    private string chat;
    private string reply;
    private string rgDataStr;
    private float nowTime;
    private int spanTime;
    private int minSpanTime;
    private int maxSpanTime;
    private static bool timeSwitch;
    public static void ReverseTimeSwitch()
    {
        timeSwitch = !timeSwitch;
    }
    //初期化関数
    private void Initialize()
    {
        chat = "";
        reply = "";
        nowTime = 0.0f;
        rgDataStr = "";
        minSpanTime = 5;
        maxSpanTime = 10;
        spanTime = Random.Range(minSpanTime, maxSpanTime);
        timeSwitch = true;
        TwitchManager.instance.Initialize();
        ReplyGenerateManager.instance.Initialize();
        VoiceGenerateManager.instance.Initialize();
    }

    private async UniTaskVoid Main()
    {
        //PythonRun();

        chat = TwitchManager.instance.GetReadChat();
        if(chat == null)
        {
            ReverseTimeSwitch();
            return;
        }

        // ReplyGenerateManager.instance.SetReplyGenerateData(chat);
        // reply = await ReplyGenerateManager.instance.GenerateReply();
        VoiceGenerateManager.instance.SetVoiceGenerateData(chat);
        await VoiceGenerateManager.instance.GenerateVoice();
        VoiceGenerateManager.instance.VoicePlay();
    }
    /// <summary>
    /// python -> youtubeコメント取得,返答
    /// *カスタムキーがあればタプルに*
    /// </summary>
    // private void PythonRun()
    // {
    //     using(Py.GIL())
    //     {
    //         dynamic pyMa = Py.Import("main");
    //         reply = pyMa.Main();
    //     }
    // }

    void Start()
    {
        Initialize();
    }

    private void Update() {
        // 喋っている間
        if(!timeSwitch)
        {

        }
        // 喋るまで
        else
        {
            if(nowTime > spanTime)
            {
                Main().Forget();
                 
                //喋るまでの時間の用意リセット
                nowTime = 0.0f;
                spanTime = Random.Range(minSpanTime, maxSpanTime);
                
                //喋るためのMainが終わるまで時間をストップ
                ReverseTimeSwitch();
            }
            else
            {
                nowTime += Time.deltaTime;
            }
        }
    }
}
