using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.TestingInterface.File;

[TestClass]
public class FileTests
{
    [TestMethod]
    public void Name()
    {
        Common.CreateSampleData().GetFile(Path.Combine(Common.ExistingDirectory, Common.ExistingFile)).Name.Should()
              .Be(Common.ExistingFile);
    }

    [TestMethod]
    public void FullPath()
    {
        Common.CreateSampleData().GetFile(Common.ExistingFileFullPath).FullPath.Should()
              .Be(Common.ExistingFileFullPath);
    }

    [TestMethod]
    public void Parent()
    {
        var fileSystem = Common.CreateSampleData();
        var expectedParent = fileSystem.GetDirectory(Common.ExistingDirectory);
        var sut = fileSystem.GetFile(Common.ExistingFileFullPath);

        sut.Parent.Should().BeSameAs(expectedParent);
    }

    [TestMethod]
    [DataRow(Common.NonExistingDirectory, false)]
    [DataRow(Common.ExistingDirectory, true)]
    public void Exists(string path, bool expectedValue)
    {
        var fileSystem = Common.CreateSampleData();
        var fullPath = Path.Combine(path, Common.ExistingFile);
        var file = fileSystem.GetFile(fullPath);
        file.Exists.Should().Be(expectedValue);
    }

    [TestMethod]
    public void ReadingExistingFile()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.ExistingFileFullPath);
        file.ReadAllText().Should().Be(Common.FileContent);
    }

    [TestMethod]
    public void ReadingNonExistingFile()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.NonExistingFileFullPath);
        file.Invoking(x => x.ReadAllText()).Should().Throw<FileNotFoundException>();
    }

    [TestMethod]
    public void WritingChangesContent()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.ExistingFileFullPath);
        file.WriteAllText("123");
        file.ReadAllText().Should().Be("123");
    }

    [TestMethod]
    public void WritingCreatesFile()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Path.Combine(Common.ExistingDirectory, "NewFile.txt"));
        file.Exists.Should().BeFalse();
        file.WriteAllText("123");
        file.Exists.Should().BeTrue();
        file.ReadAllText().Should().Be("123");
    }

    [TestMethod]
    public void WritingFileInNonExistingDirectoryCreatesDirectoryWhenRunFromTest()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.NonExistingFileFullPath);
        file.Parent.Exists.Should().BeFalse();

        file.Invoking(x => x.WriteAllText("123")).Should().NotThrow();

        file.Exists.Should().BeTrue();
        file.ReadAllText().Should().Be("123");
        file.Parent.Exists.Should().BeTrue();
    }

    [TestMethod]
    public void DeletingExistingFile()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.ExistingFileFullPath);
        file.Delete();
        file.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void DeletingNonExistingFile()
    {
        var fileSystem = Common.CreateSampleData();
        var file = fileSystem.GetFile(Common.NonExistingFileFullPath);
        file.Delete();
        file.Exists.Should().BeFalse();
    }
}