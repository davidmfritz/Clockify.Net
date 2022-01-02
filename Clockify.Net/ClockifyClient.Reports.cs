﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clockify.Net.Models.Reports;
using RestSharp;

namespace Clockify.Net
{
    public partial class ClockifyClient
    {
        /// <summary>
        /// Obtains the requested Detailed Report.
        /// </summary>
        public Task<IRestResponse<DetailedReportDto>> GetDetailedReportAsync(string workspaceId, DetailedReportRequest detailedReportRequest)
        {
            var request = new RestRequest($"workspaces/{workspaceId}/reports/detailed", Method.POST);
            request.AddJsonBody(detailedReportRequest);

            return _reportsClient.ExecutePostAsync<DetailedReportDto>(request);
        }

        /// <summary>
        /// Obtains the requested Summary Report.
        /// </summary>
        public Task<IRestResponse<SummaryReportDto>> GetSummaryReportAsync(string workspaceId, SummaryReportRequest summaryReportRequest)
        {
            if (summaryReportRequest.DateRangeStart == null) { throw new ArgumentNullException(nameof(summaryReportRequest.DateRangeStart)); }
            if (summaryReportRequest.DateRangeEnd == null) { throw new ArgumentNullException(nameof(summaryReportRequest.DateRangeStart)); }
            if (summaryReportRequest.SummaryFilter == null) { throw new ArgumentNullException(nameof(summaryReportRequest.SummaryFilter)); }
            
            var request = new RestRequest($"workspaces/{workspaceId}/reports/summary", Method.POST);
            request.AddJsonBody(summaryReportRequest);
            
            return _reportsClient.ExecuteAsync<SummaryReportDto>(request);
        }

        /// <summary>
        /// NOT IMPLEMENTED
        ///
        /// Obtains the requested Weekly Report.
        /// </summary>
        private Task<IRestResponse<List<WeeklyReportDto>>> GetWeeklyReportAsync(string workspaceId)
        {
            var request = new RestRequest($"workspaces/{workspaceId}/reports/weekly", Method.POST);
            //request.AddJsonBody(requestBody);

            return _reportsClient.ExecuteAsync<List<WeeklyReportDto>>(request, Method.POST);
        }
    }
}
