using FPVenturesZohoInventorySalesOrder.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        /// <summary>
        /// Posts Sales Order Models to ZOHO Inventory
        /// </summary>
        /// <param name="zohoInventorySalesOrderModel">Sales Order model of ZOHO Inventory</param>
        /// <param name="logger">Logger object to log error if occur</param>
        /// <returns>Response model for Sales Order</returns>
        public ZohoInventorySalesOrderResponseModel PostSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel, ILogger logger);

        /// <summary>
        /// Get Items from ZOHO Inventory
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger);

        /// <summary>
        /// Mark SalesOrder as confirmed in ZOHO Inventory
        /// </summary>
        /// <param name="salesOrderId">Sales order Id</param>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns Inventory Response</returns>
        public InventoryResponse ConfirmSalesOrder(string salesOrderId, ILogger logger);

        /// <summary>
        /// Post Invoice to the ZOHO Inventory
        /// </summary>
        /// <param name="zohoInventoryInvoiceRequestModel">ZOHO Inventory Invoice Model</param>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns ZOHO Inventory Invoice response</returns>
        public ZohoInventoryInvoiceResponseModel PostInvoice(ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel, ILogger logger);

        /// <summary>
        /// Get the contact person for the given customer
        /// </summary>
        /// <param name="customerId">Id of the customer(Intially HAWX)</param>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns Zoho Inventory Contact person</returns>
        public ZohoInventoryContactPersonResponseModel GetContactPersonFromZohoInventory(string customerId, ILogger logger);

        /// <summary>
        /// Get contacts from ZOHO Inventory
        /// </summary>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns ZOHO Inventory Contacts</returns>
        public ZohoInventoryContactsResponseModel GetContactsFromZohoInventory(ILogger logger);

        /// <summary>
        /// Generates Invoice using the SalesOrder Id
        /// </summary>
        /// <param name="zohoInventorySalesOrderResponseModel">Sales Order Reponse Model to fetch the Id</param>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns ZOHO Inventory Invoice Response</returns>
        public ZohoInventoryInvoiceResponseModel CreateInvoiceFromSalesOrder(ZohoInventorySalesOrderResponseModel zohoInventorySalesOrderResponseModel, ILogger logger);
    }
}
