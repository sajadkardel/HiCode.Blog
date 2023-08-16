using HC.Data.Entities.Common;
using HC.Data.Repositories.Contracts;
using HC.Data.Services.Contracts;
using HC.Shared.Constants;
using HC.Shared.Enums;
using HC.Shared.Markers;
using HC.Shared.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;

namespace HC.Data.Services.Implementations;

public class MediaService : IMediaService, IScopedDependency
{
    private readonly string BasePath;
	private readonly IRepository<Media> _mediaRepository;
	public MediaService(IRepository<Media> mediaRepository)
	{
        _mediaRepository = mediaRepository;
        BasePath = Assembly.GetEntryAssembly()?.FullName ?? string.Empty;
    }

    public async Task<Result> InsertMediaAsync(IFormFile file, MediaSystemType systemType, MediaFileType fileType, string? alt = null, CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length == 0) return Result.Failed("File Not Found!");

        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant().Replace(".", "");
        if (FileConstants.AllowedExtensions.Any(x => x == fileExtension) is false) return Result.Failed("File Type is invalid!");

        if (Directory.Exists(BasePath) is false) Directory.CreateDirectory(BasePath);

        string fileNameToAdd = Guid.NewGuid().ToString();
        var path = Path.Combine(BasePath, fileNameToAdd);

        // Save the file
        using var fs = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fs, cancellationToken);

        await _mediaRepository.AddAsync(new Media
        {
            Name = fileNameToAdd,
            Alt = alt,
            Size = file.Length,
            PhysicalPath = path,
            MediaSystemType = systemType,
            MediaFileType = fileType,
            MediaFileExtension = fileExtension
        });
        
        return Result.Success();
    }

    public async Task<Result<string>> GetMediaAsUrlAsync(int id, CancellationToken cancellationToken = default)
    {
        var media = await _mediaRepository.GetByIdAsync(cancellationToken, id);
        if (media is null) return Result.Failed<string>("File Not Found!");

        string path = Path.Combine(BasePath, media.PhysicalPath);
        if (string.IsNullOrEmpty(path)) return Result.Failed<string>("File Not Found!");

        return Result.Success<string>(path);
    }

    public async Task<Result<byte[]>> GetMediaAsByteAsync(int id, CancellationToken cancellationToken = default)
    {
        var media = await _mediaRepository.GetByIdAsync(cancellationToken, id);
        if (media is null) return Result.Failed<byte[]>("File Not Found!");

        string path = Path.Combine(BasePath, media.PhysicalPath);
        if (string.IsNullOrEmpty(path)) return Result.Failed<byte[]>("File Not Found!");

        byte[] fileBytes =  await File.ReadAllBytesAsync(path, cancellationToken);
        if (fileBytes is null) return Result.Failed<byte[]>("File Not Found!");

        return Result.Success(fileBytes);
    }
}
