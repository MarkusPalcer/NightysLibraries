namespace MarkusPalcer.FileSystem.Implementation;

public class FileSystem : IFileSystem
{
    public IFile GetFile(string path)
    {
        return new File(Path.GetFullPath(path));
    }

    public IDirectory GetDirectory(string path)
    {
        return new Directory(System.IO.Path.GetFullPath(path));
    }
}