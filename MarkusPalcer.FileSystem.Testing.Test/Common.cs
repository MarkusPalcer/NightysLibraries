namespace MarkusPalcer.FileSystem.Testing.Test;

public class Common
{
    public const string ExistingRoot = @"C:\";
    public const string NonExistingDirectory = @"C:\NonExisting";
    public const string ExistingDirectory = @"C:\Existing";
    public const string ExistingSubDirectory = @"C:\Existing\Child";
    public const string NonExistingFile = @"NonExisting.txt";
    public const string ExistingFile = "Existing.txt";
    public const string FileContent = "Test";
    public static string ExistingFileFullPath = Path.Combine(ExistingDirectory, ExistingFile);
    public static string NonExistingFileFullPath = Path.Combine(NonExistingDirectory, ExistingFile);

    public static FileSystem CreateSampleData()
    {
        var result = new FileSystem();
        result.CreateDirectory(ExistingDirectory);
        result.CreateDirectory(ExistingSubDirectory);
        result.CreateFile(Path.Combine(ExistingDirectory, ExistingFile), FileContent);
        return result;
    }
}