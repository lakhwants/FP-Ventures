using FPVenturesZohoInventorySalesOrder.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        public ZohoInventorySalesOrderResponseModel PostSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel, ILogger logger);
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger);
        public InventoryResponse ConfirmSalesOrder(string salesOrderId, ILogger logger);
        public ZohoInventoryInvoiceResponseModel PostInvoice(ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel, ILogger logger);
        public ZohoInventoryContactPersonResponseModel GetContactPersonFromZohoInventory(string contacts, ILogger logger);
        public ZohoInventoryContactsResponseModel GetContactsFromZohoInventory(ILogger logger);
        public ZohoInventoryInvoiceResponseModel CreateInvoiceFromSalesOrder(ZohoInventorySalesOrderResponseModel zohoInventorySalesOrderResponseModel, ILogger logger);
    }
}
