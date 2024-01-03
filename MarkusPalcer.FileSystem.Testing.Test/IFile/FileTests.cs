using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.IFile;

[TestClass]
public class FileTests
{
    [TestMethod]
    public void Name()
    {
        ((MarkusPalcer.FileSystem.IFile)Common.CreateSampleData().GetFile(Common.ExistingFileFullPath)).Name.Should()
                                                                                                .Be(Common.ExistingFile);
    }

    [TestMethod]
    public void FullPath()
    {
        ((MarkusPalcer.FileSystem.IFile)Common.CreateSampleData().GetFile(Common.ExistingFileFullPath)).FullPath.Should()
                                                                                                       .Be(Common.ExistingFileFullPath);
    }

    [TestMethod]
    public void Parent()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var expectedParent = fileSystem.GetDirectory(Common.ExistingDirectory);
        var sut = fileSystem.GetFile(Common.ExistingFileFullPath);

        sut.Parent.Should().BeSameAs(expectedParent);
    }

    [TestMethod]
    [DataRow(Common.NonExistingDirectory, false)]
    [DataRow(Common.ExistingDirectory, true)]
    public void Exists(string path, bool expectedValue)
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var fullPath = Path.Combine(path, Common.ExistingFile);
        var file = fileSystem.GetFile(fullPath);
        file.Exists.Should().Be(expectedValue);
    }

    [TestMethod]
    public void ReadingExistingFile()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.ExistingFileFullPath);
        file.ReadAllText().Should().Be(Common.FileContent);
    }

    [TestMethod]
    public void ReadingNonExistingFile()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.NonExistingFileFullPath);
        file.Invoking(x => x.ReadAllText()).Should().Throw<FileNotFoundException>();
    }

    [TestMethod]
    public void WritingChangesContent()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.ExistingFileFullPath);
        file.WriteAllText("123");
        file.ReadAllText().Should().Be("123");
    }

    [TestMethod]
    public void WritingCreatesFile()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Path.Combine(Common.ExistingDirectory, "NewFile.txt"));
        file.Exists.Should().BeFalse();
        file.WriteAllText("123");
        file.Exists.Should().BeTrue();
        file.ReadAllText().Should().Be("123");
    }

    [TestMethod]
    public void WritingFileInNonExistingDirectoryFails()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.NonExistingFileFullPath);
        file.Invoking(x => x.WriteAllText("123")).Should().Throw<DirectoryNotFoundException>();
        file.Exists.Should().BeFalse();
    }


    [TestMethod]
    public void DeletingExistingFile()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.ExistingFileFullPath);
        file.Delete();
        file.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void DeletingNonExistingFile()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.NonExistingFileFullPath);
        file.Delete();
        file.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void RequestingFileWithEmptyFileName()
    {
        var fileSystem = (IFileSystem)Common.CreateSampleData();
        var directory = fileSystem.GetDirectory(Environment.CurrentDirectory);
        directory.Invoking(x => x.GetFile(string.Empty)).Should().Throw<ArgumentException>();
    }
}