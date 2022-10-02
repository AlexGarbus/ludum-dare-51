using LudumDare51.Saving;
using Godot;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LudumDare51.AutoLoad
{
    public class SaveManager : Node
    {
        private static readonly string _filePath = ProjectSettings.GlobalizePath("user://save.box");

        public static SaveData CurrentSave { get; set; } = CurrentSave = new SaveData();

        public override void _Ready()
        {
            if (System.IO.File.Exists(_filePath))
            {
                Load();
            }
            else
            {
                Save();
            }
        }

        public static void Save()
        {
            FileStream fileStream = new FileStream(_filePath, FileMode.Create);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, CurrentSave);
            }
            catch (SerializationException e)
            {
                GD.PrintErr($"Failed to serialize. Reason: {e.Message}");
            }
            finally
            {
                fileStream.Close();
            }
        }

        public static void Load()
        {
            FileStream fileStream = new FileStream(_filePath, FileMode.Open);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CurrentSave = (SaveData)formatter.Deserialize(fileStream);
            }
            catch (SerializationException e)
            {
                GD.PrintErr($"Failed to deserialize. Reason: {e.Message}");
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}