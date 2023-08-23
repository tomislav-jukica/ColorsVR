using System;

namespace ColorAPI
{
    [Serializable]
    public class PickedColor
    {
        public PickedColor(string targetColor, string selectedColor, long userId)
        {
            TargetColor = targetColor;
            SelectedColor = selectedColor;
            UserId = userId;
        }

        public long Id;
        public string TargetColor;
        public string SelectedColor;
        public long UserId;//Foreign key
    }
}