using AutoMapper;
using MHW.Companion.Data.Store;
using MHW.Companion.Model.User;
using MHW.Companion.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MHW.Companion.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, AppDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);
            
            return new OkObjectResult(newUser.Id);
        }
    }
}
