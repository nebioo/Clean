using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ApiController
    {
        private IMapper _mapper;

        public FileController(IMapper mapper)
        {
            _mapper = mapper;
        }

    }
}
