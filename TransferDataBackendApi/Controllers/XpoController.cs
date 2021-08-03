using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransferDataBackendApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class XpoController : ControllerBase
    {
        readonly WebApiDataStoreService DataStoreService;
        public XpoController(WebApiDataStoreService dataStoreService)
        {
            this.DataStoreService = dataStoreService;
        }
        [HttpGet]
        public string Hello()
        {
            return "Use WebApiDataStoreClient to connect to this service. See also: https://docs.devexpress.com/XPO/402182/connect-to-a-data-store/transfer-data-via-rest-api";
        }
        [HttpPost]        
        public Task<OperationResult<UpdateSchemaResult>> UpdateSchema([FromQuery] bool doNotCreateIfFirstTableNotExist, [FromBody] DBTable[] tables)
        {
            return DataStoreService.UpdateSchemaAsync(doNotCreateIfFirstTableNotExist, tables);
        }
        [HttpPost]       
        public Task<OperationResult<SelectedData>> SelectData([FromBody] SelectStatement[] selects)
        {
            return DataStoreService.SelectDataAsync(selects);
        }
        [HttpPost]       
        public Task<OperationResult<ModificationResult>> ModifyData([FromBody] ModificationStatement[] dmlStatements)
        {
            return DataStoreService.ModifyDataAsync(dmlStatements);
        }
       // [HttpPost]        
       // public OperationResult<DataCacheModificationResult> ModifyDataWithCookie([FromBody] WebApiDataContainer<ModificationStatement[]> data)
       // {
       //     return DataStoreService.ModifyDataWithCookie(data);
       // }
       // [HttpPost]        
       // public OperationResult<DataCacheResult> NotifyDirtyTables([FromBody] WebApiDataContainer<string[]> data)
       // {
       //     return DataStoreService.NotifyDirtyTables(data);
       // }
       // [HttpPost]
       // //[EnableCors("XPO")]
       // public OperationResult<DataCacheResult> ProcessCookie([FromBody] DataCacheCookie data)
       // {
       //     return DataStoreService.ProcessCookie(data);
       // }
       // [HttpPost]
       // //[EnableCors("XPO")]
       // public OperationResult<DataCacheSelectDataResult> SelectDataWithCookie([FromBody] WebApiDataContainer<SelectStatement[]> data)
       // {
       //     return DataStoreService.SelectDataWithCookie(data);
       // }
       // [HttpPost]
       ////[EnableCors("XPO")]
       // public OperationResult<DataCacheUpdateSchemaResult> UpdateSchemaWithCookie([FromQuery] bool doNotCreateIfFirstTableNotExist, [FromBody] WebApiDataContainer<DBTable[]> data)
       // {
       //     return DataStoreService.UpdateSchemaWithCookie(doNotCreateIfFirstTableNotExist, data);
       // }
    }
}
