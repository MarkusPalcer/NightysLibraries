namespace MarkusPalcer.FileSystem.Implementation;

public class Directory : IDirectory
{
    private readonly DirectoryInfo _directoryInfo;

    internal Directory(string fullPath) : this(new DirectoryInfo(fullPath))
    {
    }

    internal Directory(DirectoryInfo directoryInfo)
    {
        _directoryInfo = directoryInfo;
    }

    public string FullPath => _directoryInfo.FullName;
    public string Name => _directoryInfo.Name;
    public IDirectory? Parent => _directoryInfo.Parent is null ? null : new Directory(_directoryInfo.Parent);
    public IEnumerable<IDirectory> Directories => _directoryInfo.EnumerateDirectories().Select(x => new Directory(x));
    public IEnumerable<IFile> Files => _directoryInfo.EnumerateFiles().Select(x => new File(x));
    public void Create() => _directoryInfo.Create();

    public IFile GetFile(string path) => new File(Path.GetFullPath(Path.Combine(_directoryInfo.FullName, path)));

    public IDirectory GetDirectory(string path) =>
        new Directory(Path.GetFullPath(Path.Combine(_directoryInfo.FullName, path)));

    public void Delete(bool recursive = false) => _directoryInfo.Delete(recursive);

    public bool Exists => _directoryInfo.Exists;
}