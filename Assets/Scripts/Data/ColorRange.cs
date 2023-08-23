using System;

namespace ColorAPI
{
    [Serializable]
    public class ColorRange
    {
        public ColorRange(string value, long mainColorId)
        {
            Value = value;
            MainColorId = mainColorId;
        }

        public long Id;
        public string Value;
        public long MainColorId; //Foreign key
    }
}