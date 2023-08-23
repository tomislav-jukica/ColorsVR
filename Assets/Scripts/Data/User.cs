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

        public long Id;
        public long Sex;
        public long Age;
        public long WorkStatus;
        public long Education;
        public long Profession;
        public long Eyesight;
        public long WorkScreen;
        public long SoloScreen;
        public long Nature;
        public long Location;
        public long Population;
    }
}