using Microsoft.AspNetCore.Mvc;
using ProfileService.Business.Interfaces;
using ProfileService.Business.Models;

namespace ProfileService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController(IProfileService profileService) : ControllerBase
{
    private readonly IProfileService _profileService = profileService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _profileService.GetProfilesAsync();

        return result.Success
            ? Ok(result)
            : StatusCode(500, result);
    }

    [HttpGet("{authUserId}")]
    public async Task<IActionResult> Get(string authUserId)
    {
        var result = await _profileService.GetProfileAsync(authUserId);

        return result.Success
            ? Ok(result)
            : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProfileRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _profileService.CreateProfileAsync(request);

        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut("{authUserId}")]
    public async Task<IActionResult> Update(string authUserId, UpdateProfileRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _profileService.UpdateProfileAsync(authUserId, request);

        return result.Success
            ? Ok(result)
            : NotFound(result);
    }

    [HttpDelete("{authUserId}")]
    public async Task<IActionResult> Delete(string authUserId)
    {
        var result = await _profileService.DeleteProfileAsync(authUserId);

        return result.Success
            ? Ok(result)
            : NotFound(result);
    }
}