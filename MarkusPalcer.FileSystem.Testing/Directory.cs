namespace MarkusPalcer.FileSystem.Testing;

public class Directory : IDirectory
{
    private readonly FileSystem _fileSystem;
    private readonly string _name;
    private readonly Directory? _parent;

    private readonly Dictionary<string, Directory> _directories = new();
    private readonly Dictionary<string, File> _files = new();

    public Directory(FileSystem fileSystem, string name, Directory? parent = null)
    {
        _fileSystem = fileSystem;
        _name = name.TrimEnd(Path.DirectorySeparatorChar);
        _parent = parent;
    }

    public string FullPath => _parent is null ? _name : Path.Combine(_parent.FullPath, _name);

    public string Name => _name;

    IDirectory? IDirectory.Parent => _parent;

    IEnumerable<IDirectory> IDirectory.Directories
    {
        get
        {
            if (!Exists) ThrowForFirstExistingParent("enumerate the directories in");
            return _directories.Values.Where(x => x.Exists);
        }
    }

    IEnumerable<IFile> IDirectory.Files
    {
        get
        {
            if (!Exists) ThrowForFirstExistingParent("enumerate the files in");

            return Files.Where(x => x.Exists);
        }
    }

    void IDirectory.Create()
    {
        if (Exists) return;

        DenyOperationOnFileSystemRoot("create");

        if (!_parent!.Exists) ThrowForFirstExistingParent("create");

        Exists = true;
    }

    private void ThrowForFirstExistingParent(string operation)
    {
        // Let's create a meaningful message
        var firstNonExistingParent = this;

        while (true)
        {
            if (firstNonExistingParent._parent is null)
            {
                throw new DirectoryNotFoundException(
                    $"Can't {operation} the directory {FullPath} because its file system root does not exist");
            }

            if (firstNonExistingParent._parent.Exists)
            {
                throw new DirectoryNotFoundException(
                    $"Can't {operation} the directory {FullPath} because the directory {firstNonExistingParent.FullPath} doesn't even exist.");
            }

            firstNonExistingParent = firstNonExistingParent._parent;
        }
    }

    IFile IDirectory.GetFile(string path) => GetFile(path);

    IDirectory IDirectory.GetDirectory(string path) => GetDirectory(path);

    private void DenyOperationOnFileSystemRoot(string operation)
    {
        if (_parent is null) throw new DirectoryNotFoundException($"Could not {operation} the directory {FullPath}, because this is not allowed for a file system root.");
    }

    void IDirectory.Delete(bool recursive)
    {
        DenyOperationOnFileSystemRoot("delete");

        if (!Exists)
        {
            ThrowForFirstExistingParent("delete");
        }

        ExecuteDeletion(recursive);
    }

    private void ExecuteDeletion(bool recursive)
    {
        if (recursive)
        {
            foreach (var subDirectory in _directories.Values)
            {
                subDirectory.Delete(recursive);
            }

            foreach (var files in _files.Values)
            {
                files.Delete();
            }
        }

        if (_directories.Values.Any(x => x.Exists) || _files.Values.Any(x => x.Exists))
            throw new IOException($"The directory {FullPath} cannot be deleted because it is not empty");

        Exists = false;
    }

    public bool Exists { get; private set; }

    /// <summary>
    /// Gets the parent directory.
    /// Returns <c>null</c> if this directory is a file system root
    /// </summary>
    public Directory? Parent => _parent;

    /// <summary>
    /// Gets all directory abstractions (existing and non existing) that have been created
    /// </summary>
    public IEnumerable<Directory> Directories => _directories.Values;

    /// <summary>
    /// Gets all file abstractions (existing and non existing) that have been created
    /// </summary>
    public IEnumerable<File> Files => _files.Values;

    /// <summary>
    /// Gets a directory abstraction from a path relative to the current directory
    /// Creates it as non-existing if it does not exist.
    /// </summary>
    public Directory GetDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("The path cannot be null or an empty string");

        if (path.Contains(Path.DirectorySeparatorChar))
            return _fileSystem.GetDirectory(Path.GetFullPath(Path.Combine(FullPath, path)));

        if (_directories.TryGetValue(path, out var result)) return result;
        result = new Directory(_fileSystem, path, this);
        _directories[path] = result;

        return result;
    }

    /// <summary>
    /// Gets a file abstraction.
    /// Creates it as non-existing if it does not exist.
    /// </summary>
    public File GetFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("The path cannot be null or an empty string");

        if (path.Contains(Path.DirectorySeparatorChar))
            return _fileSystem.GetFile(Path.GetFullPath(Path.Combine(FullPath, path)));

        if (_files.TryGetValue(path, out var result)) return result;
        result = new File(path, this);
        _files[path] = result;

        return result;
    }

    /// <summary>
    /// Creates the directory
    /// </summary>
    /// <param name="recursive">
    /// If set to <c>false</c> creation will fail when any parent is not created;
    /// otherwise parent directories and the root will be created if they don't exit
    /// </param>
    public void Create(bool recursive = true)
    {
        if (Exists) return;

        if (!recursive)
        {
            ((IDirectory)this).Create();
            return;
        }

        _parent?.Create();
        Exists = true;
    }

    /// <summary>
    /// Removes the directory and all its subdirectories (unless turned off)
    /// <br />
    /// Does not fail if the directory does not exist
    /// </summary>
    public void Delete(bool recursive = true)
    {
        if (!Exists) return;

        ExecuteDeletion(recursive);
    }
}