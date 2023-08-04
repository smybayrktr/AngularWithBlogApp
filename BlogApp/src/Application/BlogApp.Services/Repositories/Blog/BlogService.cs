using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.Helpers.FileHelper;
using BlogApp.Core.Utilities.Helpers.UrlHelper;
using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Services.Extensions;
using BlogApp.Services.Repositories.AppUser;
using Microsoft.AspNetCore.Http;
using IResult = BlogApp.Core.Utilities.Results.IResult;

namespace BlogApp.Services.Repositories.Blog;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IUrlHelper _urlHelper;
    private readonly IFileHelper _fileHelper;

    public BlogService(IBlogRepository blogRepository, IMapper mapper, IUserService userService,
        IUrlHelper urlHelper, IFileHelper fileHelper)
    {
        _blogRepository = blogRepository;
        _userService = userService;
        _mapper = mapper;
        _urlHelper = urlHelper;
        _fileHelper = fileHelper;
    }

    public async Task<IResult> CreateBlogAsync(CreateNewBlogRequest newBlogRequest)
    {
        var currentUser = await _userService.GetCurrentUser();
        if (!currentUser.Success) return new ErrorResult(Messages.Unauthorized, ApiStatusCodes.Unauthorized);

        var blog = newBlogRequest.ConvertToDto(_mapper);
        blog.Url = GenerateUrl(blog.Title);
        blog.UserId = currentUser.Data.Id;
        await _blogRepository.CreateAsync(blog);
        return new SuccessResult(Messages.BlogCreated, ApiStatusCodes.Created);
    }

    public async Task<IDataResult<string>> UploadBlogImage(IFormFile formFile)
    {
        if (formFile == null)
            return new ErrorDataResult<string>(ApiStatusCodes.BadRequest);

        var imagePath = await _fileHelper.UploadImage(formFile);
        imagePath = _urlHelper.AddBaseUrlToUrl(imagePath);
        return new SuccessDataResult<string>(imagePath, Messages.Uploaded, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<BlogCardResponse?>> GetBlogAsync(int id)
    {
        var blog = await _blogRepository.GetAsync(id);
        var response = blog.ConvertToDto(_mapper);
        return new SuccessDataResult<BlogCardResponse?>(response, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<UpdateBlogRequest?>> GetBlogAsUpdateBlogDtoAsync(int id)
    {
        var blog = await _blogRepository.GetAsync(id);
        var response = blog.ConvertToUpdateDto(_mapper);
        return new SuccessDataResult<UpdateBlogRequest>(response, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogsByCategoryAsync(int categoryId)
    {
        var blogs = await _blogRepository.GetBlogDtosByCategory(categoryId);
        var responses = blogs.ConvertToDto(_mapper);
        return new SuccessDataResult<IEnumerable<BlogCardResponse?>>(responses, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogsCardResponsesAsync(int pageNumber = 1)
    {
        var skip = (pageNumber - 1) * Settings.BlogPerPage;
        var blogCardDtos = (await _blogRepository.GetBlogCardDtos()).Skip(skip).Take(Settings.BlogPerPage);
        var responses = blogCardDtos.ConvertToDto(_mapper);
        return new SuccessDataResult<IEnumerable<BlogCardResponse?>>(responses, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<BlogPaginationResponse>> GetBlogsCount()
    {
        var blogsCount = (await _blogRepository.GetBlogCardDtos()).Count();
        var blogPaginationResponse = new BlogPaginationResponse
        {
            BlogsCount = blogsCount,
            TotalPage = (int)Math.Ceiling(blogsCount / (double)Settings.BlogPerPage)
        };
        return new SuccessDataResult<BlogPaginationResponse>(blogPaginationResponse, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogsByUserAsync()
    {
        var userCheck = await _userService.GetCurrentUser();
        if (!userCheck.Success)
            return new ErrorDataResult<IEnumerable<BlogCardResponse>>(Messages.Unauthorized, ApiStatusCodes.Unauthorized);

        var blogs = await _blogRepository.GetAllWithPredicateAsync(x => x.UserId == userCheck.Data.Id);
        var responses = blogs.ConvertToDto(_mapper);
        return new SuccessDataResult<IEnumerable<BlogCardResponse>>(responses, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetSavedBlogsAsync()
    {
        var userCheck = await _userService.GetCurrentUser();
        if (!userCheck.Success)
            return new ErrorDataResult<IEnumerable<BlogCardResponse>>(Messages.Unauthorized, ApiStatusCodes.Unauthorized);

        var blogCardDtos = await _blogRepository.GetSavedBlogCardDtos(userCheck.Data.Id);
        var responses = blogCardDtos.ConvertToDto(_mapper);
        return new SuccessDataResult<IEnumerable<BlogCardResponse?>>(responses, ApiStatusCodes.Ok);
    }

    private string GenerateUrl(string url)
    {
        return _urlHelper.ToSeoUrl(url) + "-" + Guid.NewGuid();
    }

    public async Task<IDataResult<BlogCardResponse?>> GetBlogByUrlAsync(string url)
    {
        var blog = await _blogRepository.GetWithPredicateAsync(u => u.Url == url.Trim());
        if (blog == null)
            return new ErrorDataResult<BlogCardResponse>(ApiStatusCodes.NotFound);

        var response = blog.ConvertToDto(_mapper);
        return new SuccessDataResult<BlogCardResponse?>(response, ApiStatusCodes.Ok);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var blogToDelete = await _blogRepository.GetAsync(id);
        if (blogToDelete == null)
            return new ErrorResult(ApiStatusCodes.NotFound);

        await _blogRepository.DeleteAsync(blogToDelete);
        return new SuccessResult(ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<bool>> UpdateAsync(UpdateBlogRequest updateBlogRequest)
    {
        var currentUser = await _userService.GetCurrentUser();
        if (!currentUser.Success)
            return new ErrorDataResult<bool>( false, Messages.Unauthorized, ApiStatusCodes.Unauthorized);

        var blogToUpdate = await _blogRepository.GetAsync(updateBlogRequest.Id);
        if (blogToUpdate == null)
            return new ErrorDataResult<bool>(false, ApiStatusCodes.NotFound);

        var checkIfBlogBelongsToCurrentUser = currentUser.Data.Id != blogToUpdate.UserId;
        if (checkIfBlogBelongsToCurrentUser)
            return new ErrorDataResult<bool>(false, Messages.Unauthorized, ApiStatusCodes.Unauthorized);

        blogToUpdate.Title = updateBlogRequest.Title;
        blogToUpdate.Body = updateBlogRequest.Body;
        blogToUpdate.CategoryId = updateBlogRequest.CategoryId;
        blogToUpdate.Url = GenerateUrl(updateBlogRequest.Title);
        blogToUpdate.Image = updateBlogRequest.Image;

        await _blogRepository.UpdateAsync(blogToUpdate);
        return new SuccessDataResult<bool>(true, Messages.Updated, ApiStatusCodes.Ok);
    }
}