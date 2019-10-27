using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lelantus.Models;
using Lelantus.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lelantus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<IEnumerable<Media>> GetAll()
        {
            return await _mediaService.GetAllAsync();
        }

        [HttpGet("{deviceId}")]
        public async Task<IEnumerable<Media>> GetAllByDevice(string deviceId)
        {
            var list = await _mediaService.GetAllByDeviceAsync(deviceId);
            return list;
        }
    }
}