using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Net.Http;
using CandyCoded.env;
using UnityEngine.Networking;

public class VoiceGenerateManager : MonoBehaviour
{
    public static VoiceGenerateManager instance;
    private static string VOICE_MAIN_URL;
    private static VoiceGenerateData voiceGeneraData;
    private static AudioSource voiceAudioSource;

    public void Initialize()
    {
        env.TryParseEnvironmentVariable("VOICE_MAIN_URL", out VOICE_MAIN_URL);
        voiceGeneraData = new VoiceGenerateData();
        voiceAudioSource = this.GetComponent<AudioSource>();
    }

    public void SetVoiceGenerateData(string res)
    {
        VoiceDataOverWrite(res);
    }

    private static void VoiceDataOverWrite(string res)
    {
        voiceGeneraData.text = res;
        voiceGeneraData.encoding = "utf-8";
        voiceGeneraData.model_id = 2;
        // voiceGeneraData.speaker_name = "suzuno";
        // voiceGeneraData.speaker_id = 0;
        // voiceGeneraData.language = "JP";
        // voiceGeneraData.auto_split = true;
    }
    
    public async UniTask GenerateVoice()
    {
        string apiURL = GetApiURL();
        var client = HttpClientLib.client;
        var request = new HttpRequestMessage(HttpMethod.Post, apiURL);
        request.Headers.Add("accept", "audio/wav");
        var response = await client.SendAsync(request);

        if(response.IsSuccessStatusCode)
        {
            Debug.Log("Connected Style-Bert-VITS2");
            OutWav(response);
        }
        else
        {
            Debug.Log("Error Style-Bert-VITS2");
        }
    }

    private string GetApiURL()
    {
        var apiURL = VOICE_MAIN_URL
                    + $"text={voiceGeneraData.text}&"
                    + $"encoding={voiceGeneraData.encoding}&"
                    + $"model_id={voiceGeneraData.model_id}";

        return apiURL;
    }

    // 音声データをAudioClipに変換
    private async void OutWav(HttpResponseMessage response)
    {
        byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
        AudioClip voice = WavUtility.ToAudioClip(audioBytes);
        voiceAudioSource.clip = voice;
    }

    // ボイス再生
    public async void VoicePlay()
    {
        voiceAudioSource.PlayOneShot(voiceAudioSource.clip);
        await UniTask.WaitWhile(() => voiceAudioSource.isPlaying);
        AituberMain.ReverseTimeSwitch();
    }

    void Awake()
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
}
