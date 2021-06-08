using FavorRepos.Bll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavorRepos.Controllers
{
    //http://localhost:50740/api/v1/Warehouse/StockItem
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        /// <summary>
        /// Retrieves stock items
        /// </summary>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("StockItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public string GetStockItemsAsync()
        {
            DataProcessing DP = new DataProcessing();
            return DP.GetHtmlInformation();
        }

        // GET
        // api/v1/Warehouse/AddRight/5
        /// <summary>
        /// Retrieves a stock item by Name
        /// </summary>
        /// <param name="Name">Stock item Name</param>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("AddRight/{Name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public string GetAddRight(string Name)
        {
            DataProcessing DP = new DataProcessing();
            return DP.AddRight(Name);
        }

        // GET
        // api/v1/Warehouse/StockItem/5
        /// <summary>
        /// Retrieves a stock item by Name
        /// </summary>
        /// <param name="Name">Stock item Name</param>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("DeleteRight/{Name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public string GetDeleteRight(string Name)
        {
            DataProcessing DP = new DataProcessing();
            return DP.DeleteRight(Name);
        }

        // GET
        // api/v1/Warehouse/StockItem/5
        /// <summary>
        /// Retrieves a stock item by Name
        /// </summary>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("GetRight")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public string GetGetRight()
        {
            DataProcessing DP = new DataProcessing();
            return DP.GetRight();
        }

        // GET
        // api/v1/Warehouse/StockItem/5
        /// <summary>
        /// Retrieves a stock item by Name
        /// </summary>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("GetLeft")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public string GetGetLeft()
        {
            DataProcessing DP = new DataProcessing();
            return DP.GetLeft();
        }
    }
}
