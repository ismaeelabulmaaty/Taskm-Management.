using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using taskm.core.Repositories.Contract;
using taskm.Core.Entites;
using taskm.Repository.Data;
using taskm.Repository.Dtos;

namespace task_management.Apis.Controllers
{
    public class TasksController : ApiBaseController
    {
        private readonly IGenericRepository<Tasks> _genericRepository;
        private readonly IGenericRepository<TeamMember> _teamMemberRebo;


        public TasksController(IGenericRepository<Tasks> genericRepository, IGenericRepository<TeamMember> TeamMemberRebo)
        {
            _genericRepository = genericRepository;
            _teamMemberRebo = TeamMemberRebo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Tasks>>> GetAllTasks()
        {
            var tasks = await _genericRepository.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Tasks>>> GetTaskById(int id)
        {

            var task = await _genericRepository.GetByIdAsync(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<Tasks>>> AddTask(TaskDto item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var TeamMber = await _teamMemberRebo.GetByIdAsync(item.TeamMemberId);
            if (TeamMber is null)
            {
                return BadRequest("pleaese Enter Team Meber Id Is Valid");
            }
            var task = new Tasks()
            {
                Name = item.Name,
                Description = item.Description,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Status = item.Status,
                TeamMemberId = item.TeamMemberId,
            };

            await _genericRepository.AddAsync(task);
            var result = await _genericRepository.CompleteAsync();
            if (result <= 0)
                return null;

            return Ok(task);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Tasks>> UpdaateTask([FromRoute] int id, [FromBody] TaskDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var TeamMber = await _teamMemberRebo.GetByIdAsync(item.TeamMemberId);
            if (TeamMber is null)
            {
                return BadRequest("pleaese Enter Team Meber Id Is Valid");
            }

            var task = await _genericRepository.GetByIdAsync(id);
            if (task is null)
            {
                return BadRequest("pleaese Enter Task Id Is Valid");
            }
            task.Name = item.Name;
            task.Description = item.Description;
            task.TeamMemberId = item.TeamMemberId;
            task.StartDate = item.StartDate;
            task.EndDate = item.EndDate;
            task.Status = item.Status;

            _genericRepository.Update(task);
            await _genericRepository.CompleteAsync();

            return Ok("Updated Success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tasks>> DeleteTask(int id , Tasks item)
        {

            var task = await _genericRepository.GetByIdAsync(id);
            if (task is null)
            {
                return BadRequest("pleaese Enter Task Id Is Valid");
            }
           
            if (item.Status==1)
            {
                return BadRequest("This Task Not Complet !");
            }

            _genericRepository.Delete(task);
            await _genericRepository.CompleteAsync();

            return Ok("Deleted Success");
        }


    }
}
