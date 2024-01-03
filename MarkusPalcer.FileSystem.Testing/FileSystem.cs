namespace MarkusPalcer.FileSystem.Testing;

public class FileSystem : IFileSystem
{
    private readonly Dictionary<string, Directory> _roots = new();

    IFile IFileSystem.GetFile(string path) => GetFile(path);

    IDirectory IFileSystem.GetDirectory(string path) => GetDirectory(path);

    public Directory GetDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("The path cannot be null or an empty string");

        var directoryStructure = GetDirectoryStructure(new DirectoryInfo(Path.GetFullPath(path)));

        // The root is e.g. "C:\"
        var rootName = directoryStructure.Pop();
        if (!_roots.TryGetValue(rootName, out var root))
        {
            root = new Directory(this, rootName);
            _roots.Add(rootName, root);
        }

        var currentDirectory = root;
        while (directoryStructure.Count > 0)
        {
            var directoryName = directoryStructure.Pop();
            currentDirectory = currentDirectory.GetDirectory(directoryName);
        }

        return currentDirectory;
    }

    public IEnumerable<Directory> EnumerateAllDirectories()
        => _roots.Values.SelectMany(EnumerateAllDirectories);

    private IEnumerable<Directory> EnumerateAllDirectories(Directory parent)
        => new[] { parent }.Concat(parent.Directories.SelectMany(EnumerateAllDirectories));

    public IEnumerable<File> EnumerateAllFiles()
        => EnumerateAllDirectories().SelectMany(x => x.Files);


    private static Stack<string> GetDirectoryStructure(DirectoryInfo info)
    {
        Stack<string> directoryStructure = new();
        var currentDirectory = info;
        while (currentDirectory is not null)
        {
            directoryStructure.Push(currentDirectory.Name);
            currentDirectory = currentDirectory.Parent;
        }

        return directoryStructure;
    }

    /// <summary>
    /// Creates the given directory and all its parent directories up to its file system root
    /// </summary>
    /// <returns>
    /// The <see cref="Directory"/>, so it can be used for further setup
    /// </returns>
    public Directory CreateDirectory(string path)
    {
        var result = GetDirectory(path);
        result.Create();
        return result;
    }

    /// <summary>
    /// Creates the given file with the given content and all its parent directories up to its file system root
    /// </summary>
    /// <returns>
    /// The <see cref="File"/>, so it can be used for further setup
    /// </returns>
    public File CreateFile(string path, string? content)
    {
        var file = new FileInfo(Path.GetFullPath(path));
        var result = CreateDirectory(file.Directory!.FullName).GetFile(file.Name);
        result.WriteAllText(content);
        return result;
    }

    public File GetFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("The path cannot be null or an empty string");

        var fileInfo = new FileInfo(Path.GetFullPath(path));
        var parent = GetDirectory(fileInfo.Directory!.FullName);

        return parent.GetFile(fileInfo.Name);
    }
}