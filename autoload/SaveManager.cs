using Godot;
using LudumDare51.Saving;

namespace LudumDare51.AutoLoad
{
    public class SaveManager : Node
    {
        public static SaveData CurrentSave { get; set; } = CurrentSave = new SaveData();

        public override void _Ready()
        {
            CurrentSave = SaveLoad.Load();
            SaveCurrent(); // TODO: Delete this
        }

        public static void SaveCurrent()
        {
            SaveLoad.Save(CurrentSave);
        }
    }
}