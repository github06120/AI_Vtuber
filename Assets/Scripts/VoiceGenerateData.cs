public class VoiceGenerateData
{
    public string text {get;set;}
    public string encoding {get;set;}
    public int model_id {get;set;}
    public string speaker_name {get;set;}
    public int speaker_id {get;set;}
    public float sdp_ratio {get;set;}
    public float noise {get;set;}
    public float noisew {get;set;}
    public float length {get;set;}
    public string language {get;set;}
    public bool auto_split {get;set;}
}