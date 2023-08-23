using System;

namespace ColorAPI
{
    [Serializable]
    public class Language
    {
        public Language(long value, long speak, User user)
        {
            Value = value;
            Speak = speak;
            User = user;
        }

        public long Id { get; set; }
        public long Value { get; set; }
        public long Speak { get; set; }
        public User User { get; set; }
    }
}