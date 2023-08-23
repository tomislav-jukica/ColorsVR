using System;

namespace ColorAPI
{
    [Serializable]
    public class ColorRange
    {
        public ColorRange(string value, PickedColor mainColor)
        {
            Value = value;
            MainColor = mainColor;
        }

        public long Id { get; set; }
        public string Value { get; set; }
        public PickedColor MainColor { get; set; }
    }
}