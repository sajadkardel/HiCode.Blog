
using HC.Shared.Enums;
using HC.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace HC.Data.Services.Contracts;

public interface IMediaService
{
    // Rename
    // Save
    // GetAsUrl
    // GetAsByte
    // GetAsBase64
    // DeleteByPath

    public Task<Result> InsertMediaAsync(IFormFile file, MediaSystemType systemType, MediaFileType fileType, string? alt = null, CancellationToken cancellationToken = default);
    public Task<Result<string>> GetMediaAsUrlAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result<byte[]>> GetMediaAsByteAsync(int id, CancellationToken cancellationToken = default);
}
