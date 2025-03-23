using BMicroservice.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    [ProducesResponseType<GetStockResponseModel>(200)]
    //Simple Type => int,double,string,bool,DateTime (Route Constraints)
    //Complex Type => Class,Record,Array,List,Dictionary
    [HttpGet("{productId:int}")]
    public IActionResult Get(int productId)
    {
        var response = new GetStockResponseModel(100);

        var responseModel = new ResponseModel<GetStockResponseModel>();
        responseModel.IsSuccess = true;
        responseModel.Data = response;


        try
        {
            return Ok(responseModel.Data);
        }
        catch (Exception e)
        {
            responseModel.Error = new ProblemDetails
            {
                Title = "ürün bulunamadı",
                Detail = "Id'si 5 olan ürün bulunamadı"
            };
            return BadRequest(responseModel.Error);
        }
    }
}