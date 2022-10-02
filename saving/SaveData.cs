using System;

namespace LudumDare51.Saving
{
    [Serializable]
    public class SaveData
    {
        public int BestScore { get; set; } = 999;
    }
}