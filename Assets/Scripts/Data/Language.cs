using System;

namespace ColorAPI
{
    [Serializable]
    public class Language
    {
        public Language(long value, long speak, long userId)
        {
            Value = value;
            Speak = speak;
            UserId = userId;
        }

        public long Id;
        public long Value;
        public long Speak;
        public long UserId; //Foreign key
    }
}