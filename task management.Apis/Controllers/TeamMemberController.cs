using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskm.core.Repositories.Contract;
using taskm.Core.Entites;
using taskm.Repository.Data;
using taskm.Repository.Dtos;

namespace task_management.Apis.Controllers
{
    public class TeamMemberController : ApiBaseController
    {
        private readonly IGenericRepository<TeamMember> _genericRepository;


        public TeamMemberController(IGenericRepository<TeamMember> genericRepository)
        {
            _genericRepository = genericRepository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TeammberDto>>> GetAllMember()
        {
            var members = await _genericRepository.GetAllAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeammberDto>> GetMemberById(int id)
        {
            var members = await _genericRepository.GetByIdAsync(id);
            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult<TeamMember>> AddMember(TeammberDto item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var task = new TeamMember()
            {
                Name = item.Name,
                Email = item.Email
            };

            await _genericRepository.AddAsync(task);
            var result = await _genericRepository.CompleteAsync();
            if (result <= 0)
                return null;

            return Ok(task);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<TeamMember>> UpdaateMember(int id, TeammberDto member)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var TeamMemb = await _genericRepository.GetByIdAsync(id);
            if (TeamMemb is null)
            {
                return BadRequest("pleaese Enter Task Id Is Valid");
            }

            TeamMemb.Name = member.Name;
            TeamMemb.Email = member.Email;

            _genericRepository.Update(TeamMemb);
            await _genericRepository.CompleteAsync();

            return Ok(member);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamMember>> DeleteMember(int id)
        {

            var member = await _genericRepository.GetByIdAsync(id);
            if (member is null)
            {
                return BadRequest("pleaese Enter Task Id Is Valid");
            }

            _genericRepository.Delete(member);
            await _genericRepository.CompleteAsync();

            return Ok("Deleted Success");

        }
    }
}
