using System;

namespace ColorAPI
{
    [Serializable]
    public class PickedColor
    {
        public PickedColor(string targetColor, string selectedColor, User user)
        {
            TargetColor = targetColor;
            SelectedColor = selectedColor;
            User = user;
        }

        public long Id { get; set; }
        public string TargetColor { get; set; }
        public string SelectedColor { get; set; }
        public User User { get; set; }
    }
}