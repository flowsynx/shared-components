namespace FlowSynx.IO;

public static class DirectoryInfoExtensions
{
    public static IEnumerable<FileInfo> FindFiles(this DirectoryInfo directoryInfo, string filter = "*", bool recursive = false)
    {
        IEnumerator<FileInfo> enumerator;
        try
        {
            enumerator = directoryInfo
                .EnumerateFiles(filter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .GetEnumerator();
        }
        catch (UnauthorizedAccessException) { yield break; }
        while (true)
        {
            try { if (!enumerator.MoveNext()) break; }
            catch (UnauthorizedAccessException) { continue; }
            yield return enumerator.Current;
        }
    }

    public static IEnumerable<DirectoryInfo> FindDirectories(this DirectoryInfo directoryInfo, string filter = "*", bool recursive = false)
    {
        IEnumerator<DirectoryInfo> enumerator;
        try
        {
            enumerator = directoryInfo
                .EnumerateDirectories(filter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .GetEnumerator();
        }
        catch (UnauthorizedAccessException) { yield break; }
        while (true)
        {
            try { if (!enumerator.MoveNext()) break; }
            catch (UnauthorizedAccessException) { continue; }
            yield return enumerator.Current;
        }
    }
}
