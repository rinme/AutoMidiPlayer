using System;
using System.IO;

namespace AutoMidiPlayer.Data;

/// <summary>
/// Centralized paths for application data storage.
/// All app data is stored in %LocalAppData%\AutoMidiPlayer
/// </summary>
public static class AppPaths
{
    /// <summary>
    /// Base application data directory: %LocalAppData%\AutoMidiPlayer
    /// </summary>
    public static readonly string AppDataDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AutoMidiPlayer");

    /// <summary>
    /// Path to the SQLite database file
    /// </summary>
    public static readonly string DatabasePath = Path.Combine(AppDataDirectory, "AutoMidiPlayer.db");

    /// <summary>
    /// Path to the crash log file
    /// </summary>
    public static readonly string CrashLogPath = Path.Combine(AppDataDirectory, "crash_log.txt");

    /// <summary>
    /// Path to the user settings file (user.config)
    /// </summary>
    public static readonly string UserConfigPath = Path.Combine(AppDataDirectory, "user.config");

    /// <summary>
    /// Ensures the app data directory exists
    /// </summary>
    public static void EnsureDirectoryExists()
    {
        if (!Directory.Exists(AppDataDirectory))
            Directory.CreateDirectory(AppDataDirectory);
    }
}
