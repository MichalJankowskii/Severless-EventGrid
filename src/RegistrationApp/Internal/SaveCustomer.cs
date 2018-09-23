namespace RegistrationApp.Internal
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Microsoft.WindowsAzure.Storage.Table;
    using Models;

    public static class SaveCustomer
    {
        [FunctionName("SaveCustomer")]
        public static async Task Run(
            [QueueTrigger("tostorecustomer", Connection = "registrationstorage_STORAGE")] Customer customer,
            [Table("customers", Connection = "registrationstorage_STORAGE")] CloudTable customersTable,
            ILogger log)
        {
            log.LogInformation($"SaveCustomer function processed: {customer.Name} {customer.Surname}");
            customer.PartitionKey = "AzureTest";
            customer.RowKey = Guid.NewGuid().ToString();

            TableOperation insertOperation = TableOperation.Insert(customer);
            await customersTable.ExecuteAsync(insertOperation);
        }
    }
}
