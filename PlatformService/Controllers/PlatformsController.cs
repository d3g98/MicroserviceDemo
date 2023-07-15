using AutoMapper;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlatformRepo _flatformRepo;
        private readonly ICommandDataClient _commandDataClient;
        public PlatformsController(IPlatformRepo flatformRepo, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _mapper = mapper;
            _flatformRepo = flatformRepo;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var m = _flatformRepo.GetAllFlatform();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(m));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var m = _flatformRepo.GetFlatformById(id);
            if (m == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlatformReadDto>(m));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlatformCreateDto model)
        {
            var flatFormModel = _mapper.Map<Platform>(model);

            _flatformRepo.CreateFlatform(flatFormModel);
            _flatformRepo.SaveChange();

            var flatformReadDto = _mapper.Map<PlatformReadDto>(flatFormModel);

            try
            {
                Console.WriteLine("--> Send data to CommandService");
                await _commandDataClient.SendPlatformToCommand(flatformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }
            return CreatedAtAction(nameof(GetById), new { Id = flatformReadDto.Id }, flatformReadDto);
        }
    }
}