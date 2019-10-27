using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lelantus.Models;
using Lelantus.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lelantus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IEnumerable<Device>> Get()
        {
            return await _deviceService.GetAllAsync();
        }
    }
}