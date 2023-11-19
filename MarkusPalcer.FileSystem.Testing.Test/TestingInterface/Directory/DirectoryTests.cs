using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.TestingInterface;

[TestClass]
public class DirectoryTests
{
    [TestMethod]
    public void CreateRecursively()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory(Common.NonExistingDirectory).GetDirectory("SubDirectory");

        sut.Create();

        fileSystem.GetDirectory(Common.NonExistingDirectory).Exists.Should().BeTrue();
        sut.Exists.Should().BeTrue();
    }

    [TestMethod]
    public void CreateNonRecursively()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory(Common.NonExistingDirectory).GetDirectory("SubDirectory");

        sut.Invoking(x => x.Create(false)).Should().Throw<DirectoryNotFoundException>();

        fileSystem.GetDirectory(Common.NonExistingDirectory).Exists.Should().BeFalse();
        sut.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void ListsAllFiles()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory(Common.ExistingDirectory);
        sut.GetFile(Common.NonExistingFile).Exists.Should().BeFalse();
        sut.GetFile(Common.ExistingFile).Exists.Should().BeTrue();
        sut.Files.Select(x => x.Name).Should().BeEquivalentTo(Common.ExistingFile, Common.NonExistingFile);
    }


}