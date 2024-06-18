using System.Collections.Generic;

[System.Serializable]
public class ReplyData
{
    public string utterance;
    public BestResponse bestResponse;
}

[System.Serializable]
public class BestResponse
{
    public string utterance;
    public List<string> options;
    public string topic;
    public bool isAutoResponse;
    public object extensions;
}
