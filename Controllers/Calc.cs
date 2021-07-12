﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S = System.Net.HttpStatusCode;
using System.Threading;
using AedIsracartWebApi.Operations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aed_isracart_ahuva.Controllers
{

    public class CalcRequest
    {
        public int Id { get; set; }
        public double ArgX { get; set; }
        public double ArgY { get; set; }
        public string Operation { get; set; }
    }
    public class CalcDto
    {
        public int Id { get; set; }
        public double ArgX { get; set; }
        public double ArgY { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
        public string Error { get; set; }
        public DateTime? Current { get; set; } = DateTime.Now;
    }

    [Route("api/calc")]
    [ApiController]
    public class Calc : ControllerBase
    {
        //static int IDX = 0;
        //static string[] Operations { get; set; } =
        //    new string[] { "+", "-", "*", "/" };

        static readonly Dictionary<int, CalcDto> CalcHistory =
            new Dictionary<int, CalcDto>();

        readonly IOperationService OpSrv;

        public Calc(IOperationService ops)
        {
            OpSrv = ops;
        }

        //// GET: api/<Calc> returns operations

        /// <summary>
        /// GET: api/calc/operations 
        /// </summary>
        /// <returns>List op possible operations as strins</returns>
        [HttpGet("operations")]
        public ActionResult<List<string>> GetOperations()
        {
            return Ok(OpSrv.Signs);
        }

        /// <summary>
        /// GET api/calc/history
        /// Retrieves the caulation's history as 
        /// </summary>
        /// <returns>List of CalcResult instances</returns>
        [HttpGet("history")]
        public ActionResult<List<CalcDto>> GetHistory()
        {
            return Ok(CalcHistory.Values.ToList());
        }



        /// <summary>
        /// POST api/calc/{id} calculates the operation by CalcOperation 
        /// supplied in body {argX,argY,Operation} , 
        /// saves result in internal store
        /// </summary>
        /// <param name="calcReq"></param>
        /// <returns>CalcResult or BadRequest</returns>
        [HttpPost]
        public  ActionResult<CalcDto> Post([FromBody] CalcRequest calcReq)
        {
            string _error = "";
            double _dret = OpSrv.TryCalculate(calcReq.ArgX, calcReq.ArgY, calcReq.Operation, out _error); 

            CalcDto ret = new CalcDto()
            {
                Id = calcReq.Id,
                ArgX = calcReq.ArgX,
                ArgY = calcReq.ArgY,
                Operation = calcReq.Operation,
                Result = _dret,
                Error = _error
            };

         //   ret.Id = Interlocked.Increment(ref IDX);
            if(ret.Error.Length <= 0)
            {
                CalcHistory[ret.Id] = ret;
            }
   

            return Ok(ret);
        }

       
        /// <summary>
        /// DELETE: api/calc/{id} instance of the history
        /// </summary>
        /// <param name="id">Identifier of history element</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            bool b = CalcHistory.ContainsKey(id);
            if (b)
            {
                CalcHistory.Remove(id);
            }
            return b;
        }
    }
}
