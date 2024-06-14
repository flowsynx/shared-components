namespace FlowSynx.IO;

public static class LongExtensions
{
    public static string ToString(this long? size, bool? applyFormat = true)
    {
        return size is null ? $"{0:0.##}" : ToString(size.Value, applyFormat);
    }

    public static string ToString(this long size, bool? applyFormat = true)
    {
        if (applyFormat is null or false) return $"{size:0.##}";
        if (size < 0) return "-" + ToString(-size, applyFormat);

        string[] sizes = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };
        var order = 0;
        var decimalSize = (decimal)size;

        while (decimalSize >= 1024 && order < sizes.Length - 1)
        {
            decimalSize /= 1024;
            order++;
        }
        return $"{decimalSize:0.##} {sizes[order]}";
    }
}