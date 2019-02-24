﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebApplication.Tests
{
    public class CreateBug
    {
        readonly string _uri;
        readonly string _personalAccessToken;
        readonly string _project;

        /// <summary>
        /// Constructor. Manually set values to match your organization. 
        /// </summary>
        public CreateBug()
        {
            _uri = "https://dev.azure.com/getratheeshar";
            _personalAccessToken = "2r2r43hf5oehi2jzosmsn6h44yyy73odlpjg7oydpqanlvu35msa";
            _project = "DevopsHackathon";
        }

        /// <summary>
        /// Create a bug using the .NET client library
        /// </summary>
        /// <returns>Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem</returns>    
        public WorkItem CreateBugUsingClientLib()
        {
            Uri uri = new Uri(_uri);
            string personalAccessToken = _personalAccessToken;
            string project = _project;

            VssBasicCredential credentials = new VssBasicCredential("", _personalAccessToken);
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            //add fields and their values to your patch document
            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Title",
                    Value = "Registration Errors"
                }
            );

            //patchDocument.Add(
            //    new JsonPatchOperation()
            //    {
            //        Operation = Operation.Add,
            //        Path = "/fields/Microsoft.VSTS.TCM.ReproSteps",
            //        Value = "Registration Page Email Validation Failed"
            //    }
            //);

            //patchDocument.Add(
            //    new JsonPatchOperation()
            //    {
            //        Operation = Operation.Add,
            //        Path = "/fields/Microsoft.VSTS.Common.Priority",
            //        Value = "1"
            //    }
            //);

            //patchDocument.Add(
            //    new JsonPatchOperation()
            //    {
            //        Operation = Operation.Add,
            //        Path = "/fields/Microsoft.VSTS.Common.Severity",
            //        Value = "2 - High"
            //    }
            //);
            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.IterationPath",
                    Value = "DevopsHackathon\\Sprint 1"
                }
            );

            VssConnection connection = new VssConnection(uri, credentials);
            WorkItemTrackingHttpClient workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                WorkItem result = workItemTrackingHttpClient.CreateWorkItemAsync(patchDocument, project, "Bug").Result;

                Console.WriteLine("Bug Successfully Created: Bug #{0}", result.Id);

                return result;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error creating bug: {0}", ex.InnerException.Message);
                return null;
            }
        }
    }
}
