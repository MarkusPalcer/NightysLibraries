namespace MarkusPalcer.FileSystem.Implementation;

public class FileSystem : IFileSystem
{
    public IFile GetFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("The path cannot be null or an empty string");

        return new File(Path.GetFullPath(path));
    }

    public IDirectory GetDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("The path cannot be null or an empty string");

        return new Directory(Path.GetFullPath(path));
    }
}