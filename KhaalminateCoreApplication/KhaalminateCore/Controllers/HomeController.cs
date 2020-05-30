using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhaalminateCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KhaalminateCore.Controllers
{
#pragma warning disable KApp
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HomeController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly KhaalminateDbContext DbContext;

        public HomeController(ILogger<HomeController> logger, KhaalminateDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }
#pragma warning restore KApp

        // GET
        // api/v1/Warehouse/Invoice

        /// <summary>
        /// Retrieves stock items
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="lastEditedBy">Last edit by (user id)</param>
        /// <param name="colorID">Color id</param>
        /// <param name="outerPackageID">Outer package id</param>
        /// <param name="supplierID">Supplier id</param>
        /// <param name="unitPackageID">Unit package id</param>
        /// <returns>A response with stock items list</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Invoice")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetInvoicesAsync(int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetInvoicesAsync));

            var response = new PagedResponse<Invoice>();

            try
            {
                // Get the "proposed" query from repository
                var query = DbContext.GetInvoices();

                // Set paging values
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                // Get the total rows
                response.ItemsCount = await query.CountAsync();

                // Get the specific page from database
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, response.PageCount, response.ItemsCount);

                Logger?.LogInformation("The stock items have been retrieved successfully.");
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetInvoicesAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Warehouse/Invoice/5

        /// <summary>
        /// Retrieves a stock item by ID
        /// </summary>
        /// <param name="id">Stock item id</param>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Invoice/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetInvoiceAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetInvoiceAsync));

            var response = new SingleResponse<Invoice>();

            try
            {
                // Get the stock item by id
                response.Model = await DbContext.GetInvoiceNumberAsync(new Invoice(id));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetInvoiceAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // POST
        // api/v1/Warehouse/Invoice/

        /// <summary>
        /// Creates a new stock item
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="201">A response as creation of stock item</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Invoice")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostInvoiceAsync([FromBody]PostInvoicesRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostInvoiceAsync));

            var response = new SingleResponse<Invoice>();

            try
            {
                var existingEntity = await DbContext
                    .GetInvoiceNumberAsync(new Invoice { Invno = request.Invno });

                if (existingEntity != null)
                    ModelState.AddModelError("Invno", "Invoice Number already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext.Add(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostInvoiceAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // PUT
        // api/v1/Warehouse/Invoice/5

        /// <summary>
        /// Updates an existing stock item
        /// </summary>
        /// <param name="id">Stock item ID</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update stock item result</returns>
        /// <response code="200">If stock item was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("Invoice/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutInvoiceAsync(int id, [FromBody]PutInvoicesRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutInvoiceAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetInvoiceNumberAsync(new Invoice(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Set changes to entity
                entity.Invno = request.Invno;

                // Update entity in repository
                DbContext.Update(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutInvoiceAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // DELETE
        // api/v1/Warehouse/Invoice/5

        /// <summary>
        /// Deletes an existing stock item
        /// </summary>
        /// <param name="id">Stock item ID</param>
        /// <returns>A response as delete stock item result</returns>
        /// <response code="200">If stock item was deleted successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("Invoice/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteInvoiceAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteInvoiceAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetInvoiceNumberAsync(new Invoice(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Remove entity from repository
                DbContext.Remove(entity);

                // Delete entity in database
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteInvoiceAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
