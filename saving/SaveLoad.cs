using Godot;
using System.Text.Json;

namespace LudumDare51.Saving
{
    public static class SaveLoad
    {
        private const string FILE_PATH = "user://save.box";

        public static void Save(SaveData saveData)
        {
            File file = new File();
            file.Open(FILE_PATH, File.ModeFlags.Write);

            string jsonString = JsonSerializer.Serialize(saveData);
            file.StoreLine(jsonString);

            file.Close();
        }

        public static SaveData Load()
        {
            File file = new File();

            if (!file.FileExists(FILE_PATH))
            {
                return new SaveData();
            }

            file.Open(FILE_PATH, File.ModeFlags.Read);

            string jsonString = file.GetLine();
            SaveData saveData = JsonSerializer.Deserialize<SaveData>(jsonString);
            
            file.Close();
            return saveData;
        }
    }
}