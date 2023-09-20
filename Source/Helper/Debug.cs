namespace TakeHome.Source.Helper
{
    public static class Debug
    {
        public static void Log<T>(T message) { Console.WriteLine(message); }
        public static void LogWarning<T>(T message) { }// Console.WriteLine($"@->{message}");}
        public static void LogHeader<T>(T message) { Console.WriteLine($"##############  {message}  ##############"); }
        public static void LogBlank() { Console.WriteLine(); }
    }
}
