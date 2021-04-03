using System;
using System.IO;
using System.Text.Json;

namespace SortTheBallsGameVariant9
{
    public static class SaveLoadController
    {
        private static string PathToSave
        {
            get
            {
                var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "SortTheBalls");
                if(!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                return Path.Combine(directoryPath, "savefile.json");
            }
        }

        public static GameSave LoadGameSave(Action<Exception> errorDelegate)
        {
            try
            {
                string jsonFile = File.ReadAllText(PathToSave);
                GameSave saveFile = JsonSerializer.Deserialize<GameSave>(jsonFile);
                return saveFile;
            }
            catch (Exception e)
            {
                errorDelegate(e);
                return null;
            }
        }

        public static bool SaveGameSave(GameSave save, Action<Exception> errorDelegate)
        {
            try
            {
                var json = JsonSerializer.Serialize(save);
                File.WriteAllText(PathToSave, json);
                return true;
            }
            catch (Exception e)
            {
                errorDelegate(e);
                return false;
            }
        }

        public static bool CanLoadGame()
        {
            return File.Exists(PathToSave);
        }
    }
}
