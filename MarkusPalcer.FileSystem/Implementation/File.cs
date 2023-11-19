namespace MarkusPalcer.FileSystem.Implementation;

public class File : IFile
{
    private readonly FileInfo _fileInfo;

    internal File(string fullPath) : this(new FileInfo(fullPath))
    {
    }

    internal File(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
    }

    public string Name => _fileInfo.Name;

    public string FullPath => _fileInfo.FullName;

    public IDirectory Parent => new Directory(_fileInfo.Directory!);

    public bool Exists => _fileInfo.Exists;

    public void WriteAllText(string? content) => System.IO.File.WriteAllText(_fileInfo.FullName, content);

    public string ReadAllText() => System.IO.File.ReadAllText(_fileInfo.FullName);

    public void Delete() => _fileInfo.Delete();
}