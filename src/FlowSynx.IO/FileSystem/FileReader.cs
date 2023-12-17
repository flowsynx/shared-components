using EnsureThat;
using FlowSynx.IO.Exceptions;
using Microsoft.Extensions.Logging;

namespace FlowSynx.IO.FileSystem;

public class FileReader : IFileReader
{
    private readonly ILogger<FileReader> _logger;

    public FileReader(ILogger<FileReader> logger)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        _logger = logger;
    }

    public string Read(string path)
    {
        try
        {
            return File.ReadAllText(path);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in read data from path '{path}'. Message: {ex.Message}");
            throw new FileReaderException(ex.Message);
        }
    }
}
