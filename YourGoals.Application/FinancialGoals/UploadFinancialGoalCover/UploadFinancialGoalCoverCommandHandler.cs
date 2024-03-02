using System.Net;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Core.Repositories;
using YourGoals.Core.Results.Errors;
using YourGoals.Domain.FinancialGoals.Errors;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Errors;

namespace YourGoals.Application.FinancialGoals.UploadFinancialGoalCover;

public sealed class UploadFinancialGoalCoverCommandHandler : IRequestHandler<UploadFinancialGoalCoverCommand, Result>
{
    private readonly ILogger<UploadFinancialGoalCoverCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFinancialGoalRepository _financialGoalRepository;

    private readonly string _staticFilesPath;

    public UploadFinancialGoalCoverCommandHandler(
        ILogger<UploadFinancialGoalCoverCommandHandler> logger,
        IHostEnvironment environment,
        IUnitOfWork unitOfWork,
        IFinancialGoalRepository financialGoalRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _financialGoalRepository = financialGoalRepository;

        _staticFilesPath = Path.Combine(environment.ContentRootPath, "wwwroot");
    }

    public async Task<Result> Handle(UploadFinancialGoalCoverCommand request, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(request.FinancialGoalId, cancellationToken);

        if (financialGoal is null)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.NotFound, HttpStatusCode.NotFound));

        var coverFileFullPath
            = GetCoverImagePaths(_staticFilesPath, financialGoal.Name);

        var convertResult = ConvertBase64ToImage(request.CoverImage);

        if (!convertResult.Success)
            return Result.Fail(new HttpStatusCodeError(convertResult.Errors[0]));

        var buffer = convertResult.Value;

        var saveResult = await SaveCoverImage(coverFileFullPath, buffer);

        if (!saveResult.Success)
            return Result.Fail(new HttpStatusCodeError(saveResult.Errors[0]));

        financialGoal.AddCoverImage(coverFileFullPath);

        _financialGoalRepository.Update(financialGoal);

        var updated = await _unitOfWork.SaveChangesAsync() > 0;

        if (!updated)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.CannotBeUpdated));

        return Result.Ok();
    }

    public async Task<Result> SaveCoverImage(string coverImageFileFullPath, byte[] buffer)
    {
        try
        {
            using var memoryStream = new MemoryStream(buffer);
            using var fileStream = new FileStream(coverImageFileFullPath, FileMode.Create);
            await memoryStream.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            var message = "Cover could not be saved";

            _logger.LogError(ex, message);

            return Result.Fail(new Error(ex.Message, message));
        }

        return Result.Ok();
    }

    private static string GetCoverImagePaths(string staticFilesPath, string financialGoalName)
    {
        var name = financialGoalName.Trim();

        var coverImageFileName = $"{DateTime.Now:yyyyMMddHHmm}-{name}.jpeg";

        var coverFilesDirectory = Path.Combine("images", "financialGoalCovers");

        var fullDirectoryPath = Path.Combine(staticFilesPath, coverFilesDirectory);

        var coverImageFileFullPath = Path.Combine(fullDirectoryPath, coverImageFileName);

        if (!Directory.Exists(fullDirectoryPath))
            Directory.CreateDirectory(fullDirectoryPath);

        return coverImageFileFullPath;
    }

    private Result<byte[]> ConvertBase64ToImage(string base64Image)
    {
        byte[] buffer = [];

        try
        {
            buffer = Convert.FromBase64String(base64Image);
        }
        catch (Exception ex)
        {
            var message = "Image could not be converted";

            _logger.LogError(ex, message);

            return Result.Fail<byte[]>(new HttpStatusCodeError(FinancialGoalErrors.InvalidBase64Image, HttpStatusCode.BadRequest));
        }

        return Result.Ok(buffer);
    }
}