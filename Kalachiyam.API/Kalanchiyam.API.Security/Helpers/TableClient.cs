using Kalanchiyam.API.Security.Entity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalanchiyam.API.Security.Helpers
{
    public static class TableClient
    {
        private static string tableConnection = "Storageconnection";
        private static string tableName = "Tablename";
        public static async void CreateNewTable(CloudTable table)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(tableConnection);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable cloudTable = tableClient.GetTableReference(tableName);
            CreateNewTable(cloudTable);
            if (!await table.CreateIfNotExistsAsync())
            {
                return;
            }

        }
        public static async void InsertRecordToTable(CustomerEntity customerEntity)
        {
            if (customerEntity != null)
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(tableConnection);
                CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
                CloudTable cloudTable = tableClient.GetTableReference(tableName);
                TableOperation tableOperation = TableOperation.Insert(customerEntity);
                await cloudTable.ExecuteAsync(tableOperation);

            }

        }
        public static async void UpdateRecordInTable(CustomerEntity customerEntity)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(tableConnection);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);
            CustomerEntity custEntity = await RetrieveRecord(table, customerEntity.CustomerType, Convert.ToString(customerEntity.CustomerID));
            if (customerEntity != null)
            {
                custEntity.CustomerDetails = customerEntity.CustomerDetails;
                custEntity.CustomerName = customerEntity.CustomerName;
                TableOperation tableOperation = TableOperation.Replace(custEntity);
                await table.ExecuteAsync(tableOperation);

            }

        }
        public static async void DeleteRecordinTable(string customerType, string customerID)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(tableConnection);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);
            CustomerEntity customerEntity = await RetrieveRecord(table, customerType, customerID);
            if (customerEntity != null)
            {
                TableOperation tableOperation = TableOperation.Delete(customerEntity);
                await table.ExecuteAsync(tableOperation);

            }

        }
        public static async Task<CustomerEntity> RetrieveRecord(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation tableOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);
            return tableResult.Result as CustomerEntity;
        }

        public static async Task<CustomerEntity> getRecords(string partitionKey, string rowKey)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(tableConnection);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);
            TableOperation tableOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);
            return tableResult.Result as CustomerEntity;
        }
    }
}
