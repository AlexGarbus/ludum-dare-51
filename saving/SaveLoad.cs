using Godot;
using System.Text;
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
            byte[] raw = Encoding.ASCII.GetBytes(jsonString);
            string base64String = Marshalls.RawToBase64(raw);
            file.StoreLine(base64String);

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

            string base64String = file.GetLine();
            byte[] raw = Marshalls.Base64ToRaw(base64String);
            string jsonString = Encoding.ASCII.GetString(raw);
            SaveData saveData = JsonSerializer.Deserialize<SaveData>(jsonString);
            
            file.Close();
            return saveData;
        }
    }
}