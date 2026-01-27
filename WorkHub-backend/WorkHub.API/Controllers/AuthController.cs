using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkHub.Application;

namespace WorkHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterHandler _registerHandler;
        private readonly LoginHandler _loginHandler;

        public AuthController(RegisterHandler registerHandler, LoginHandler loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _registerHandler.HandleAsync(RegisterMappings.ToCommand(request), cancellationToken);

            var apiResult = result.ToApiResult();

            return apiResult.IsSuccess ? Ok(apiResult) : BadRequest(apiResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _loginHandler.HandleAsync(LoginMappings.ToCommand(request), cancellationToken);
            
            var apiResult = result.ToApiResult();

            return apiResult.IsSuccess ? Ok(apiResult) : BadRequest(apiResult);
        }
    }
}
