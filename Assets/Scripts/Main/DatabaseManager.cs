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
         string requestString = $"https://game-chuck.com/rijekavr/users.php?" +
            $"Sex={user.Sex}" +
            $"&Age={user.Age}" +
            $"&WorkStatus={user.WorkStatus}" +
            $"&Education={user.Education}" +
            $"&Profession={user.Profession}" +
            $"&Eyesight={user.Eyesight}" +
            $"&WorkScreen={user.WorkScreen}" +
            $"&SoloScreen={user.SoloScreen}" +
            $"&Nature={user.Nature}" +
            $"&Location={user.Location}" +
            $"&Population={user.Population}";

        var response = await _client.PostAsync(requestString, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var id = int.Parse(responseString);
        
        PostLanguages(_languages, id);
        PostPickedColor(_pickedColor, id);        
    }

    private async void PostLanguages(List<ColorAPI.Language> languages, int userId)
    {
        foreach (var language in languages)
        {
            language.UserId = userId;
            var json = JsonUtility.ToJson(language);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string requestString = $"https://game-chuck.com/rijekavr/languages.php?" +
                $"Speak={language.Speak}" +
                $"&UserId={language.UserId}" +
                $"&Value={language.Value}";
            var response = await _client.PostAsync(requestString, content);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }

    private async void PostPickedColor(PickedColor pickedColor, int userId)
    {
        pickedColor.UserId = userId;
        var json = JsonUtility.ToJson(pickedColor);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        string requestString = $"https://game-chuck.com/rijekavr/pickedcolors.php?" +
            $"SelectedColor='{pickedColor.SelectedColor}'" +
            $"&TargetColor='{pickedColor.TargetColor}'" +
            $"&UserId={userId}";
        var response = await _client.PostAsync(requestString, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var id = int.Parse(responseString);

        PostColorRanges(_colorRanges, id);
    }

    private async void PostColorRanges(List<ColorRange> colorRanges, int pickedColorId)
    {
        foreach (var colorRange in colorRanges)
        {
            colorRange.MainColorId = pickedColorId;
            var json = JsonUtility.ToJson(colorRange);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string requestString = $"https://game-chuck.com/rijekavr/colorranges.php?" +
                $"MainColorId={colorRange.MainColorId}" +
                $"&Value='{colorRange.Value}'";
            var response = await _client.PostAsync(requestString, content);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
