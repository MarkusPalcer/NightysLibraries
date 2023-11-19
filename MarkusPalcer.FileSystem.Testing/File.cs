namespace MarkusPalcer.FileSystem.Testing;

public class File : IFile
{
    private readonly Directory _parent;
    private MemoryStream? _data;

    internal File(string name, Directory parent)
    {
        Name = name;
        _parent = parent;
    }

    public string Name { get; }

    public string FullPath => $"{_parent.FullPath}{Path.DirectorySeparatorChar}{Name}";

    public IDirectory Parent => _parent;

    public bool Exists => _data is not null;

    void IFile.WriteAllText(string? content)
    {
        if (!_parent.Exists) throw new DirectoryNotFoundException();

        WriteAllText(content);
    }

    public void WriteAllText(string? content)
    {
        if (!_parent.Exists) _parent.Create(true);

        var newContent = content ?? string.Empty;
        _data = new MemoryStream();
        var writer = new StreamWriter(_data);
        writer.Write(newContent.AsMemory());
        writer.Flush();
    }

    public string ReadAllText()
    {
        if (_data is null) throw new FileNotFoundException();

        _data.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(_data);
        return reader.ReadToEnd();
    }

    public void Delete()
    {
        _data = null;
    }
}