using System.Diagnostics;

namespace SSP
{

    public class GameMode
    {
        public enum Mode
        {
            Normal,
            Rival,
            NCARS
        }

        public static readonly Dictionary<Mode, String> GameModeMap = new()
        { // no duplicate values are allowed
            { Mode.Normal, "Normal (Random)" },
            { Mode.Rival, "Rival" },
            { Mode.NCARS, "Never Change A Running System"}
        };

        public static readonly Mode[] Modes = GameModeMap.Keys.ToArray();
        public static readonly String[] ModeNames = GameModeMap.Values.ToArray();

        public static Mode GameModeFromString(String Name)
        {
            try
            {
                return GameModeMap.First(x => x.Value == Name).Key;
            }
            catch (Exception E)
            {
                Debug.WriteLine(
                    $"Warning: GameModeFromString failed for {Name}. Returning default GameMode (GameMode.Normal). \n " +
                    $"Reason: Could not find KeyValue Pair in GameModeMap. \n" +
                    $"Exception: {E.Message}"
                    );
                return Mode.Normal;
            }
        }
    }
}
