using ColorAPI;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private static readonly HttpClient _client = new HttpClient();
    private User _postedUser = new(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);

    private List<ColorAPI.Language> _languages;
    private PickedColor _pickedColor;
    private List<ColorRange> _colorRanges;

    public void SendData(User user, List<ColorAPI.Language> languages, PickedColor pickedColor, List<ColorRange> colorRanges)
    {
        PostUser(user);
        _languages = languages;
        _pickedColor = pickedColor;
        _colorRanges = colorRanges;
        //PostLanguages(languages);
        //PostPickedColor(pickedColor);
        //PostColorRanges(colorRanges);
    }

    private async void PostUser(User user)
    {
        var json = JsonUtility.ToJson(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://localhost:7052/api/user", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var id = int.Parse(responseString);
        
        PostLanguages(_languages, id);
    }

    private async void PostLanguages(List<ColorAPI.Language> languages, int userId)
    {
        foreach (var language in languages)
        {
            language.UserId = userId;
            var json = JsonUtility.ToJson(language);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7052/api/language", content);
            var responseString = await response.Content.ReadAsStringAsync();
            print(responseString);
        }
    }

    private async void PostPickedColor(PickedColor pickedColor)
    {
        var json = JsonUtility.ToJson(pickedColor);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://localhost:7052/api/picked_color", content);
        var responseString = await response.Content.ReadAsStringAsync();
        print(responseString);
    }

    private async void PostColorRanges(List<ColorRange> colorRanges)
    {
        foreach (var colorRange in colorRanges)
        {
            var json = JsonUtility.ToJson(colorRange);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7052/api/color_range", content);
            var responseString = await response.Content.ReadAsStringAsync();
            print(responseString);
        }
    }
}
