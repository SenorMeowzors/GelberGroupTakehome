namespace TakeHome.Source.Helper
{
    public static class Debug
    {
        public static bool Simple;

        public static void Log<T>(T message, bool force = false) { if (!Simple || force) Console.WriteLine(message); }
        public static void LogWarning<T>(T message) { Console.WriteLine($"@->{message}");}
        public static void LogHeader<T>(T message, bool force = false) { if (!Simple || force) Log($"##############  {message}  ##############", force); }
        public static void LogBlank() { if (!Simple) Console.WriteLine(); }
    }
}
