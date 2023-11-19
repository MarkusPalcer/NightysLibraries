using System.Runtime.InteropServices;
using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.IDirectory;

[TestClass]
public class DirectoryTests
{
    [TestMethod]
    public void SimpleNameDoesNotContainPathSeparator()
    {
        var sut = Common.CreateSampleData();
        sut.EnumerateAllDirectories().Should()
           .AllSatisfy(x => x.Name.Should().NotContain(Path.DirectorySeparatorChar.ToString()));
    }

    [TestMethod]
    public void SimpleNameIsLastPartOfPath()
    {
        var sut = Common.CreateSampleData();
        sut.EnumerateAllDirectories().Should()
           .AllSatisfy(x => x.Name.Should().Be(x.FullPath.Split(Path.DirectorySeparatorChar).Last()));
    }

    [TestMethod]
    public void ParentOfExistingDirectory()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.ExistingDirectory);
        sut.Parent.Should().NotBeNull();
        sut.Parent!.FullPath.Should().Be("C:");
    }

    [TestMethod]
    public void ParentOfNonExistingDirectory()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.NonExistingDirectory);
        sut.Parent.Should().NotBeNull();
        sut.Parent!.FullPath.Should().Be("C:");
    }

    [TestMethod]
    public void OnlyEnumeratesExistingDirectories()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.ExistingRoot);
        sut.Directories.Select(x => x.FullPath).Should().BeEquivalentTo(Common.ExistingDirectory);
    }

    [TestMethod]
    public void OnlyEnumeratesExistingFiles()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.ExistingDirectory);
        sut.GetFile("NonExistingFile");

        sut.Files.Select(x => x.FullPath).Should()
           .BeEquivalentTo(Path.Combine(Common.ExistingDirectory, Common.ExistingFile));
    }
}