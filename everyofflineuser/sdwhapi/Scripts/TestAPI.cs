using UnityEngine;

public class TestAPI : MonoBehaviour
{

    private DiscordWebhookAPI api;

    private void Awake()
    {
        // Перебираем все объекты с типом DiscordWebhookAPI
        foreach (var obj in FindObjectsOfType<DiscordWebhookAPI>())
        {
            // Проверяем, содержит ли объект необходимую переменную ArchorName с нужным значением
            if (obj.ArchorName == "ArchorName")
            {
                // Делаем что-то со найденным объектом
                api = obj;
            }
        }
        if (api == null)
        {
            Debug.Log("Объект с нужным Якорем не найден!");
        }
    }

    private void Start()
    {
        api.SendMessage(true, $"Test Webhook API v{api.verAPI}", null, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", true);
        Debug.Log(api.GetMessage("1207366756080029716"));
        api.EditMessage(true, "1207366756080029716", "Testing Edit Message Function...");
        //api.DeleteMessage("1207364230647382036");
    }
}
