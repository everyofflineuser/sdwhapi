using UnityEngine;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;

// By NeAleks AKA everyofflineuser, 2024, sdwhapidocs.neserver.space.

public class DiscordWebhookAPI : MonoBehaviour
{
    [SerializeField]
    public string ArchorName = "ArchorName";
    [SerializeField]
    private string WebHookID = "ID";
    [SerializeField]
    private string WebHookToken = "TOKEN";

    public string verAPI { get; private set; } = "1.9";

    public string lastmsg { get; private set; }

    private string url;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        url = $"https://discord.com/api/webhooks/{WebHookID}/{WebHookToken}";
    }

    public void SendMessage(bool debug, string content, string username = null, string avatar_url = null, bool tts = false, bool getmsgdata = false)
    {
        WebRequest request;
        if (getmsgdata) request = WebRequest.Create(url + "?wait=true");
        else request = WebRequest.Create(url);
        request.Method = "POST";

        ExecuteWebhookObject execute = new ExecuteWebhookObject();
        if (content != null) execute.content = content;
        if (username != null) execute.username = username;
        if (avatar_url != null) execute.avatar_url = avatar_url;
        if (tts != false) execute.tts = tts;

        var json = JsonConvert.SerializeObject(execute);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);

        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;

        using var reqStream = request.GetRequestStream();
        reqStream.Write(byteArray, 0, byteArray.Length);

        using var response = request.GetResponse();
        if (debug) Debug.Log(((HttpWebResponse)response).StatusDescription);

        using var respStream = response.GetResponseStream();

        using var reader = new StreamReader(respStream);
        string data = reader.ReadToEnd();
        if (debug) Debug.Log(data);
        if (getmsgdata) lastmsg = data;
    }

    public string GetMessage(string msgid)
    {
        var request = WebRequest.Create(url + $"/messages/{msgid}");
        request.Method = "GET";

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();
        return data.ToString();
    }

    public void EditMessage(bool debug, string msgid, string content)
    {
        var request = WebRequest.Create(url + $"/messages/{msgid}");
        request.Method = "PATCH";

        ExecuteWebhookObject execute = new ExecuteWebhookObject();
        if (content != null) execute.content = content;

        var json = JsonConvert.SerializeObject(execute);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);

        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;

        using var reqStream = request.GetRequestStream();
        reqStream.Write(byteArray, 0, byteArray.Length);

        using var response = request.GetResponse();
        if (debug) Debug.Log(((HttpWebResponse)response).StatusDescription);

        using var respStream = response.GetResponseStream();

        using var reader = new StreamReader(respStream);
        string data = reader.ReadToEnd();
        if (debug) Debug.Log(data);
    }

    public void DeleteMessage(bool debug, string msgid)
    {
        var request = WebRequest.Create(url + $"/messages/{msgid}");
        request.Method = "DELETE";

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();
        if (debug) Debug.Log(data + "\n" + ((HttpWebResponse)webResponse).StatusDescription);
    }

    public void Destroy()
    {
        Destroy(this);
    }
}

public class ExecuteWebhookObject
{
    public string content;
    public string username;
    public string avatar_url;
    public bool tts;
}
