using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using that2dollar.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace that2dollar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        IRabbitMqServer Rabbit;
        public RabbitController(IRabbitMqServer rabbit)
        {
            Rabbit = rabbit;
        }
        // GET: api/<RabbitController>

        [HttpGet("receive")]
        public string[] Get()
        {
            return  Rabbit.Receive();
        }

        // GET api/<RabbitController>/5
        [HttpGet("send/{msg}")]
        public void Get(string msg)
        {
            Rabbit.Send( msg);
        }
        [HttpGet("count")]
        public int GetMessageCount()
        {
            return Rabbit.GetMessageCount();
        }

        // POST api/<RabbitController>

    }



}
