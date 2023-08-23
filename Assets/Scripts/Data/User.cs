using System;

namespace ColorAPI
{
    [Serializable]
    public class User
    {
        public User(long sex, long age, long workStatus, long education, long profession, long eyesight, long workScreen, long soloScreen, long nature, long location, long population)
        {
            Sex = sex;
            Age = age;
            WorkStatus = workStatus;
            Education = education;
            Profession = profession;
            Eyesight = eyesight;
            WorkScreen = workScreen;
            SoloScreen = soloScreen;
            Nature = nature;
            Location = location;
            Population = population;
        }

        public long Id { get; set; }
        public long Sex { get; set; }
        public long Age { get; set; }
        public long WorkStatus { get; set; }
        public long Education { get; set; }
        public long Profession { get; set; }
        public long Eyesight { get; set; }
        public long WorkScreen { get; set; }
        public long SoloScreen { get; set; }
        public long Nature { get; set; }
        public long Location { get; set; }
        public long Population { get; set; }
    }
}