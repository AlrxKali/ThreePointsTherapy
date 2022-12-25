using API.DTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository _wdRepo;
        private readonly IMapper _mapper;

        public WordsController(IWordRepository wdRepo, IMapper mapper)
        {
            _wdRepo = wdRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetWords()
        {
            var listWords = _wdRepo.GetWords();

            var listWordDto = new List<WordDTO>();

            foreach (var word in listWords)
            {
                listWordDto.Add(_mapper.Map<WordDTO>(word));
            }
            return Ok(listWordDto);
        }
    }
}
