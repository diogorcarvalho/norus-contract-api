using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorusContract.Api.Models;
using NorusContract.Domain.Entities;
using NorusContract.Domain.Validation;
using NorusContract.Services;

namespace NorusContract.Api.Controller
{
  [ApiController]
  public class ContractController : ControllerBase
  {
    protected readonly IContractService _contractService;

    public ContractController([FromServices] IContractService contractService)
    {
      _contractService = contractService;
    }

    [HttpPost, Authorize, Route("v1/contracts")]
    public ActionResult<Contract> Post([FromBody] Contract contract)
    {
      try
      {
        var _contract = _contractService.Save(contract);        
        return Ok(_contract);
      }
      catch (BusinessException exception)
      {
        return StatusCode(500, new { message = exception.Message });
      }
    }

    [HttpGet, Authorize, Route("v1/contracts/{contractId}")]
    public ActionResult<Contract> Get([FromRoute] int contractId)
    {
      try
      {
        var _contract = _contractService.Get(contractId);        
        return Ok(_contract);
      }
      catch (BusinessException exception)
      {
        return StatusCode(500, new { message = exception.Message });
      }
    }

    [HttpGet, Authorize, Route("v1/contracts")]
    public ActionResult<IEnumerable<Contract>> GetAll()
    {
      try
      {
        var _contracts = _contractService.GetAll();
        return Ok(_contracts);
      }
      catch (BusinessException exception)
      {
        return StatusCode(500, new { message = exception.Message });
      }
    }

    [HttpGet, Authorize, Route("v1/contracts/search")]
    public ActionResult<IEnumerable<Contract>> Search([FromQuery] string keyword)
    {
      try
      {
        var _contracts = _contractService.Search(keyword);
        return Ok(_contracts);
      }
      catch (BusinessException exception)
      {
        return StatusCode(500, new { message = exception.Message });
      }
    }

    [HttpPatch, Authorize, Route("v1/contracts")]
    public ActionResult<Contract> Patch([FromBody] PatchRequest patch)
    {
      try
      {
        var _contract = _contractService.Patch(patch);
        return Ok(_contract);
      }
      catch (BusinessException exception)
      {
        return StatusCode(500, new { message = exception.Message });
      }
    }

    [HttpDelete, Authorize, Route("v1/contracts/{contractId}")]
    public ActionResult Delete([FromRoute] int contractId)
    {
      try
      {
        _contractService.Remove(contractId);
        return Ok();
      }
      catch (BusinessException exception)
      {
        return StatusCode(500, new { message = exception.Message });
      }
    }
  }
}