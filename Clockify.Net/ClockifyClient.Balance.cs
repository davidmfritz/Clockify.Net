﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Clockify.Net.Models;
using Clockify.Net.Models.Balance;
using RestSharp;

namespace Clockify.Net;

public partial class ClockifyClient
{
	/// <summary>
	/// Get Balance by Policy Id.
	/// </summary>
	/// <returns></returns>
	public async Task<Response<BalancesDto>> GetBalanceByPolicyAsync(string workspaceId, string policyId, BalanceRequest? balance = null)
	{
		var request = new RestRequest($"workspaces/{workspaceId}/time-off/balance/policy/{policyId}");
		if (balance != null)
		{
			if (balance.Page is {} balancePage) request.AddQueryParameter("page", balancePage);
			if (balance.PageSize is { } balancePageSize) request.AddQueryParameter("page-size", balancePageSize);
			if (balance.Sort != null) request.AddQueryParameter("sort", balance.Sort.ToString());
			if (balance.SortOrder != null) request.AddQueryParameter("sort-order", balance.SortOrder.ToString());
		}
		return Response<BalancesDto>.FromRestResponse(await _client.ExecuteGetAsync<BalancesDto>(request).ConfigureAwait(false));
	}

    /// <summary>
    /// Update Balance on workspace.
    /// </summary>
    public async Task<Response> UpdateBalanceAsync(string workspaceId, string policyId, UpdateBalanceRequest balance)
	{
		if (balance == null) throw new ArgumentNullException(nameof(balance));
		if (balance.UserIds == null || !balance.UserIds.Any()) throw new ArgumentNullException(nameof(balance.UserIds));
		if (balance.Value == null) throw new ArgumentNullException(nameof(balance.Value));

		var request = new RestRequest($"workspaces/{workspaceId}/time-off/balance/policy/{policyId}");
		request.AddJsonBody(balance);
		return Response.FromRestResponse(await _client.ExecuteAsync(request, Method.Patch).ConfigureAwait(false));
	}

    /// <summary>
    /// Get Balance by User Id.
    /// </summary>
    /// <returns></returns>
    public async Task<Response<BalancesDto>> GetBalanceByUserAsync(string workspaceId, string userId, BalanceRequest? balance = null)
	{
		var request = new RestRequest($"workspaces/{workspaceId}/time-off/balance/user/{userId}");
		if (balance != null)
		{
			if (balance.Page is {} balancePage) request.AddQueryParameter("page", balancePage);
			if (balance.PageSize is { } balancePageSize) request.AddQueryParameter("page-size", balancePageSize);
			if (balance.Sort != null) request.AddQueryParameter("sort", balance.Sort.ToString());
			if (balance.SortOrder != null) request.AddQueryParameter("sort-order", balance.SortOrder.ToString());
		}
		return Response<BalancesDto>.FromRestResponse(await _client.ExecuteGetAsync<BalancesDto>(request).ConfigureAwait(false));
	}
}