using Lelantus.Models;
using Lelantus.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;

namespace Lelantus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaHubController : ControllerBase
    {
        private IHubContext<MediaHub, ITypedHubClient> _hubContext;

        public MediaHubController(IHubContext<MediaHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public string Post([FromBody] Media msg)
        {
            string retMessage = string.Empty;
            try
            {
                _hubContext.Clients.All.BroadcastMessage("Unknown", JsonConvert.SerializeObject(msg));
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return retMessage;
        }
    }
}