using UnityEngine;

public class TestAPI : MonoBehaviour
{

    private DiscordWebhookAPI api;

    private void Awake()
    {
        api = FindObjectOfType<DiscordWebhookAPI>().GetComponent<DiscordWebhookAPI>();
    }

    private void Start()
    {
        api.SendMessage(true, $"Test Webhook API v{api.verAPI}", null, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", true);
        Debug.Log(api.GetMessage("1207366756080029716"));
        api.EditMessage(true, "1207366756080029716", "Testing Edit Message Function...");
        //api.DeleteMessage("1207364230647382036");
    }
}
