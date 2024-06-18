using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Policy;
using CandyCoded.env;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.PlayerLoop;

public class TwitchManager : MonoBehaviour
{
    public static TwitchManager instance;
    private static string URL;
    private static int PORT;
    private static TcpClient twitch;
    private static StreamReader reader;
    private static StreamWriter writer;
    private static string user;
    private static string oauth;
    private static string channel = "hanjoudesu";
    private string message;
    private string refMessage;

    public void Initialize()
    {
        env.TryParseEnvironmentVariable("TWITCH_URL", out URL);
        env.TryParseEnvironmentVariable("TWITCH_PORT", out PORT);
        env.TryParseEnvironmentVariable("TWITCH_USER", out user);
        env.TryParseEnvironmentVariable("TWITCH_OAUTH", out oauth);
        // ↓ テスト時はコメントアウトしてchannelを自ら設定
        // env.TryParseEnvironmentVariable("TWITCH_CHANNEL", out channel);

        ConnectTwitch();
        //チャットまでの余計なラインをとばすために
        while(true)
        {
            message = reader.ReadLine();
            if(message.Contains("End of"))
            {
                break;
            }
        }
    }

    private static void ConnectTwitch()
    {
        twitch = new TcpClient(URL, PORT);
        reader = new StreamReader(twitch.GetStream());
        writer = new StreamWriter(twitch.GetStream());

        writer.WriteLine("PASS " + oauth);
        writer.WriteLine("NICK " + user.ToLower());
        writer.WriteLine("JOIN #" + channel.ToLower());
        writer.Flush();
    }

    public string GetReadChat()
    {
        //前回とコメントが被っていないか
        if(refMessage == message)
        {
            return null;
        }
        else
        {
            refMessage = message;

            if(message.Contains("PRIVMSG"))
            {
                //コメント抽出
                int splitePoint = message.IndexOf(":", 1);
                var msg = message.Substring(splitePoint + 1);
                // //コメントしたユーザ抽出
                // splitePoint = message.IndexOf("!", 1);
                // string user = message.Substring(1, splitePoint - 1);
                if(msg.Contains("/"))
                {
                    return null;
                }
                else
                {
                    return msg;
                }
            }
        }
        return null;
    }

    private async void GetChats()
    {
        if(twitch != null && twitch.Available > 0)
        {
            await GetChat();

            if(message.Contains("PING"))
            {
                message.Replace("PING", "PONG");
                writer.WriteLine(message);
                writer.Flush();
            }
        }
        if(twitch == null || !twitch.Connected)
        {
            ConnectTwitch();
        }
    }
    private async UniTask GetChat()
    {
        message = reader.ReadLine();
        await UniTask.DelayFrame(1);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        GetChats();
    }
}
