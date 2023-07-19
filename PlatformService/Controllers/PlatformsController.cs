using AutoMapper;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IPlatformRepo _flatformRepo;
        private readonly ICommandDataClient _commandDataClient;
        public PlatformsController(IPlatformRepo flatformRepo, IMapper mapper, ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _mapper = mapper;
            _messageBusClient = messageBusClient;
            _flatformRepo = flatformRepo;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var m = _flatformRepo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(m));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var m = _flatformRepo.GetPlatformById(id);
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

            _flatformRepo.CreatePlatform(flatFormModel);
            _flatformRepo.SaveChange();

            var flatformReadDto = _mapper.Map<PlatformReadDto>(flatFormModel);


            //send sync message
            //try
            //{
            //    Console.WriteLine("--> Send data to CommandService");
            //    await _commandDataClient.SendPlatformToCommand(flatformReadDto);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            //}

            //send async message
            try
            {
                Console.WriteLine("--> Send data to CommandService");
                var platformPublisher = _mapper.Map<PlatformPublishedDto>(flatformReadDto);
                platformPublisher.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublisher);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetById), new { Id = flatformReadDto.Id }, flatformReadDto);
        }
    }
}